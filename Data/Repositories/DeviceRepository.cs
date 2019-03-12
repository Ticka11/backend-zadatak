using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd_zadatak.Dtos;
using BackEnd_zadatak.Dtos.DeviceDtos;
using BackEnd_zadatak.Helpers;
using BackEnd_zadatak.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_zadatak.Data.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DeviceRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        //kreira uredjaj
        public void CreateDevice(Device device)
        {
            _context.Devices.Add(device);
        }

        public void UpdateDevice(Device device)
        {
            //vrsi update uredjaja i njegovih osobina, ukoliko ih ima
            if (device.DevicePropertyValues != null)
            {
                _context.DevicePropertyValues.UpdateRange(device.DevicePropertyValues);
            }
            _context.Devices.Update(device);
        }

        public async Task DeleteDevice(int id)
        {
            var device = await GetDevice(id);
            if (device != null)
            {
                // blok se izvrsava ako zeljeni uređaj postoji
                // ukoliko uredjaj ima osobine, brisemo i svaku od njih

                if (device.DevicePropertyValues != null)
                {
                    foreach (var property in device.DevicePropertyValues)
                    {
                        _context.DevicePropertyValues.Remove(property);
                    }
                }

                _context.Devices.Remove(device);
            }
            else
            {
                //izbacuje gresku ukoliko uredjaj sa zadatim id-em ne postoji u bazi
                throw new Exception("Device does not exist");
            }

        }

        // vraca uredjaj zajedno sa vrijednostima osobina, kao i vrijednostima osobina roditeljskog
        public async Task<Device> GetDevice(int id)
        {
            var device = await _context.Devices
                        .Include(d => d.DevicePropertyValues)
                        .Include(d => d.DeviceType).ThenInclude(d => d.DeviceTypeProperty)
                        .Include(d => d.DeviceType).ThenInclude(d => d.ParentDeviceType).ThenInclude(d => d.DeviceTypeProperty)
                        .FirstOrDefaultAsync(d => d.Id == id);

            return device;

        }
        //vraca uredjaje u zavisnosti od vrijednosti parametara
        public async Task<DeviceListDto> GetDevicesByCriteria(DeviceParams deviceParams)
        {
            var devices = _context.Devices
                                    .Include(d => d.DevicePropertyValues)
                                    .Include(d => d.DeviceType).ThenInclude(d => d.DeviceTypeProperty)
                                    .Include(d => d.DeviceType).ThenInclude(d => d.ParentDeviceType).ThenInclude(d => d.DeviceTypeProperty)
                                    .AsQueryable();

            //ako postoji Name parametar, vraca uredjaje koji sadrze zadato ime
            if (!string.IsNullOrEmpty(deviceParams.Name))
            {
                var searchName = deviceParams.Name.Trim().ToLowerInvariant();
                devices = devices.Where(d => d.Name.Contains(searchName));
            }

            //ako postoji Type parametar, vraca uredjaje odgovarajuceg tipa
            if (!string.IsNullOrEmpty(deviceParams.Type))
            {
                var searchType = deviceParams.Type.Trim().ToLowerInvariant();
                devices = devices.Where(d => d.DeviceType.Name.Contains(searchType));

                //ukoliko postoje Type i PropertyValue parametri
                //vraca uredjaje tog tipa sa odgovarajucim vrijednostima osobina
                if (!string.IsNullOrEmpty(deviceParams.PropertyValue))
                {
                    var searchPropertyValue = deviceParams.PropertyValue.Trim().ToLowerInvariant();
                    devices = devices.Where(d => d.DevicePropertyValues
                                    .Any(p => p.Value.Contains(searchPropertyValue)));
                }

            }

            //ako postoji compare operator (<,>,<=,>=) i cijena, vraca odgovarajuce uredjaje

            if (!string.IsNullOrEmpty(deviceParams.CompareOperator) && deviceParams.Price != null)
            {
                switch (deviceParams.CompareOperator)
                {
                    case "<":
                        devices = devices.Where(d => d.Price < deviceParams.Price);
                        break;
                    case ">":
                        devices = devices.Where(d => d.Price > deviceParams.Price);
                        break;
                    case ">=":
                        devices = devices.Where(d => d.Price >= deviceParams.Price);
                        break;
                    case "<=":
                        devices = devices.Where(d => d.Price <= deviceParams.Price);
                        break;
                    case "=":
                        devices = devices.Where(d => d.Price == deviceParams.Price);
                        break;

                    default:
                        devices = devices.OrderByDescending(d => d.Price);
                        break;
                }
            }

            //izvrsava paging na osnovu zadatih vriijednosti parametara
            var pagedDevices = await devices
                                 .Skip(deviceParams.PageSize * (deviceParams.PageNumber - 1))
                                 .Take(deviceParams.PageSize)
                                 .ToListAsync();

            var devicesMapped = _mapper.Map<IEnumerable<DeviceToReturnDto>>(pagedDevices);

            var objectToReturn = new DeviceListDto(devicesMapped, devices.Count());

            return objectToReturn;

        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}