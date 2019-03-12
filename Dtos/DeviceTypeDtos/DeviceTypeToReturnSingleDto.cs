using System.Collections.Generic;

namespace BackEnd_zadatak.Dtos.DeviceTypeDtos
{
    public class DeviceTypeToReturnSingleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentDeviceType { get; set; }
        public int? ParentId { get; set; }
        public ICollection<TypePropertyToReturnDto> DeviceTypeProperties { get; set; }
        public ICollection<TypePropertyToReturnDto> ParentTypeProperties { get; set; }
        public ICollection<DeviceTypeToReturnListDto> ChildrenDeviceType { get; set; }

    }
}