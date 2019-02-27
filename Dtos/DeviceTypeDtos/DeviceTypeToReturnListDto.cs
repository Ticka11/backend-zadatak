using System.Collections.Generic;

namespace BackEnd_zadatak.Dtos.DeviceTypeDtos
{
    public class DeviceTypeToReturnListDto
    {
        public string Name { get; set; }
        public ICollection<DeviceTypeToReturnListDto> ChildrenDeviceType { get; set; }
    }
}