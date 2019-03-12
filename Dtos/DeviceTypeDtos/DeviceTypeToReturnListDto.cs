using System.Collections.Generic;

namespace BackEnd_zadatak.Dtos.DeviceTypeDtos
{
    public class DeviceTypeToReturnListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ICollection<DeviceTypeToReturnListDto> ChildrenDeviceType { get; set; }
        public ICollection<TypePropertyToReturnDto> ParentTypeProperties { get; set; }
        public ICollection<TypePropertyToReturnDto> DeviceTypeProperties { get; set; }

    }
}