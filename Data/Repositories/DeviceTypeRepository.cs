using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd_zadatak.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_zadatak.Data.Repositories
{
    public class DeviceTypeRepository : IDeviceTypeRepository
    {
        private readonly DataContext _context;
        public DeviceTypeRepository(DataContext context)
        {
            _context = context;
        }

        //kreira tip uredjaja
        public void CreateDeviceType(DeviceType deviceType)
        {
            _context.DeviceTypes.Add(deviceType);
        }
        public void UpdateDeviceType(DeviceType deviceType)
        {
            //izvrsava update tipa uredjaja i njegovih osobina, ukoliko ih ima
            if (deviceType.DeviceTypeProperty != null)
            {
                _context.DeviceTypeProperties.UpdateRange(deviceType.DeviceTypeProperty);
            }
            _context.DeviceTypes.Update(deviceType);
        }

        public async Task DeleteDeviceType(int id)
        {
            var deviceTypeToDelete = await GetDeviceType(id);

            //brisanje tipa uredjaja nije dozvoljeno ukoliko u bazi postoji makar jedan uredjaj
            //koji je tog tipa

            if (deviceTypeToDelete != null)
            {
                var devicesOfCurrentType = await _context.Devices
                        .AnyAsync(d => d.DeviceType.Name == deviceTypeToDelete.Name);

                if (devicesOfCurrentType)
                    throw new Exception("You cannot delete device type");

                // ukoliko nema, brisemo ga zajedno sa njegovim osobinama 

                if (deviceTypeToDelete.DeviceTypeProperty != null)
                {
                    foreach (var typeProperty in deviceTypeToDelete.DeviceTypeProperty)
                    {
                        _context.DeviceTypeProperties.Remove(typeProperty);
                    }
                }

                _context.DeviceTypes.Remove(deviceTypeToDelete);
            }
            else
            {
                //izbacuje gresku ukoliko tip uredjaja sa zadatim id-em ne postoji u bazi
                throw new Exception("Device type does not exist");
            }
        }

        // vraca tip uredjaja zajedno sa njegovim osobinama,
        // i osobinama njegovog roditeljskog tipa, ukoliko ga ima
        public async Task<DeviceType> GetDeviceType(int id)
        {
            var deviceType = await _context.DeviceTypes
                        .Include(d => d.DeviceTypeProperty)
                        .Include(d => d.ChildrenDeviceType)
                        .Include(d => d.ParentDeviceType).ThenInclude(p => p.DeviceTypeProperty)
                        .FirstOrDefaultAsync(d => d.Id == id);

            return deviceType;
        }

        //vraca sve tipove uredjaja hijerarhijski
        public async Task<IEnumerable<DeviceType>> GetDeviceTypes()
        {
            var list = await _context.DeviceTypes
                    .Include(d => d.ChildrenDeviceType)
                    .Include(d => d.DeviceTypeProperty)
                    .Include(d => d.ParentDeviceType).ThenInclude(p => p.DeviceTypeProperty)
                    .ToListAsync();

            return list.Where(d => d.ParentId == null);
        }

        //pozivamo ovu funkciju kada zelimo da se promjene izvrse na bazi
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}