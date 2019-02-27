using System.Collections.Generic;

namespace BackEnd_zadatak.Dtos.DeviceTypeDtos
{
    public class DeviceTypeToReturnSingleDto
    {
        public string Name { get; set; }
        public string ParentDeviceType { get; set; }
        public ICollection<TypePropertyToReturnDto> DeviceTypeProperty { get; set; }
        public ICollection<TypePropertyToReturnDto> ParentTypeProperty { get; set; }
    }
}