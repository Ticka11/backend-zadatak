using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd_zadatak.Data.Repositories;
using BackEnd_zadatak.Dtos;
using BackEnd_zadatak.Dtos.DeviceTypeDtos;
using BackEnd_zadatak.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTypesController : ControllerBase
    {
        private readonly IDeviceTypeRepository _repo;
        private readonly IMapper _mapper;
        public DeviceTypesController(IDeviceTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

    /// <summary>
    /// Creates or updates device type and its properties depending on the presence of id
    /// </summary>
    /// <param name="deviceTypeDto">when creating device type, set id field of device type and its properties to null, or delete them
    /// but don't forget to provide appropriate ids when update</param>

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateDeviceType(DeviceTypeToCreateOrUpdate deviceTypeDto)
        {
            if(deviceTypeDto == null)
                return BadRequest();

            if(deviceTypeDto.Id == null)
            {
                //ovaj blok se izvrsava ukoliko je u pitanju kreiranje tipa uredjaja
                var deviceType = _mapper.Map<DeviceType>(deviceTypeDto);
                _repo.CreateDeviceType(deviceType);

                if(await _repo.SaveAll())
                {
                    var deviceTypeFromRepo = await _repo.GetDeviceType(deviceType.Id);
                    var deviceTypeToReturn = _mapper.Map<DeviceTypeToReturnSingleDto>(deviceTypeFromRepo); 

                    return CreatedAtRoute("GetDeviceType", new {id = deviceTypeFromRepo.Id}, deviceTypeToReturn);
                }

                throw new Exception("Creating device type failed on save");
            }
            else
            {
                //ovaj blok se izvrsava ukoliko je u pitanju update tipa uredjaja
                var deviceTypeToUpdate = _mapper.Map<DeviceType>(deviceTypeDto);
                
                _repo.UpdateDeviceType(deviceTypeToUpdate);

                if(await _repo.SaveAll())
                {
                    return NoContent();
                }
                
                throw new Exception("Updating device type failed on save");

            }
        }

    /// <summary>
    /// Deletes the device type by its Id.
    /// </summary>
    /// <param name="id">the id of the device type we want to delete</param>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceType(int id)
        {
            await _repo.DeleteDeviceType(id);
            
            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception("Error deleting the device type");

        }

    /// <summary>
    /// Retrieves the device type by its Id.
    /// </summary>
    /// <param name="id">the id of the device type we want to return</param>
    /// <returns>returns device type with its properties</returns>

        [HttpGet("{id}", Name="GetDeviceType")]
        public async Task<IActionResult> GetDeviceType(int id)
        {
            var deviceTypeFromRepo = await _repo.GetDeviceType(id);

            if(deviceTypeFromRepo == null) 
                return BadRequest("Device type does not exist");

            var deviceTypeToReturn = _mapper.Map<DeviceTypeToReturnSingleDto>(deviceTypeFromRepo);

            return Ok(deviceTypeToReturn);
        }

    /// <summary>
    /// Returns device types
    /// </summary>

        [HttpGet]
        public async Task<IActionResult> GetDeviceTypes()
        {
            var deviceTypesFromRepo = await _repo.GetDeviceTypes();
            var devicesToReturn = _mapper.Map<IEnumerable<DeviceTypeToReturnListDto>>(deviceTypesFromRepo);

            return Ok(devicesToReturn);
        }

    }
}