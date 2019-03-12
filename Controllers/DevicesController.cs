using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd_zadatak.Data.Repositories;
using BackEnd_zadatak.Helpers;
using BackEnd_zadatak.Models;
using BackEnd_zadatak.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceRepository _repo;
        private readonly IMapper _mapper;
        public DevicesController(IDeviceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

    /// <summary>
    /// Creates or updates device and its properties depending on the presence of id
    /// </summary>
    /// <param name="deviceDto">when creating device, set id field of device and its properties to null, or delete them
    /// but don't forget to provide appropriate ids when update</param>

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateDevice(DeviceForCreationOrUpdate deviceDto)
        {
            if(deviceDto == null)
                return BadRequest();

            if(deviceDto.Id == null)
            {
                //ovaj blok se izvrsava ukoliko je u pitanju kreiranje uredjaja
                var device = _mapper.Map<Device>(deviceDto);

                _repo.CreateDevice(device);

                if(await _repo.SaveAll())
                {
                    var deviceFromRepo = await _repo.GetDevice(device.Id);

                    var deviceToReturn = _mapper.Map<DeviceToReturnDto>(deviceFromRepo);
                    return CreatedAtRoute("GetDevice", new {Id = deviceFromRepo.Id}, deviceToReturn);

                }
                throw new Exception("Creating device failed on save");
                
            }
            else
            {
                //ovaj blok se izvrsava ukoliko je u pitanju update uredjaja
                var deviceToUpdate = _mapper.Map<Device>(deviceDto);
                
                _repo.UpdateDevice(deviceToUpdate);

                if(await _repo.SaveAll())
                {
                    return NoContent();
                }
                
                throw new Exception("Updating device failed on save");
            }

        }

        
    /// <summary>
    /// Retrieves the device by its Id.
    /// </summary>
    /// <param name="id">the id of the device we want to return</param>
    /// <returns>returns device with its properties</returns>

        [HttpGet("{id}", Name="GetDevice")]
        public async Task<IActionResult> GetDevice(int id)
        {
            var deviceFromRepo = await _repo.GetDevice(id);

            if(deviceFromRepo == null) 
                return BadRequest("Device does not exist");

            var deviceToReturn = _mapper.Map<DeviceToReturnDto>(deviceFromRepo);
            return Ok(deviceToReturn);
        }

    /// <summary>
    /// Deletes the device by its Id
    /// </summary>
    /// <param name="id">the id of the device we want to delete</param>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            await _repo.DeleteDevice(id);
            
            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception("Error deleting the device");
        }

    /// <summary>
    /// Returns devices based on specified criteria
    /// </summary>

        [HttpGet]
        public async Task<IActionResult> GetDevicesByCriteria([FromQuery] DeviceParams deviceParams)
        {
            var devicesFromRepo = await _repo.GetDevicesByCriteria(deviceParams);
            // var devicesToReturn = _mapper.Map<IEnumerable<DeviceToReturnDto>>(devicesFromRepo.Devices);

            return Ok(devicesFromRepo);
        }
    
    }
}