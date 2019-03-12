using System.Collections.Generic;
using BackEnd_zadatak.Models;

namespace BackEnd_zadatak.Dtos.DeviceDtos
{
    public class DeviceListDto
    {

        public DeviceListDto(IEnumerable<DeviceToReturnDto> Devices, int TotalDevicesCount)
        {
            this.Devices = Devices;
            this.TotalDevicesCount = TotalDevicesCount;
        }
        public IEnumerable<DeviceToReturnDto> Devices { get; set; }
        public int TotalDevicesCount { get; set; }
    }
}