using System.Collections.Generic;
using BackEnd_zadatak.Dtos.DeviceTypeDtos;

namespace BackEnd_zadatak.Dtos
{
    public class DeviceTypeToCreateOrUpdate
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ICollection<TypePropertyForCreationOrUpdate> DeviceTypeProperties { get; set; }
    }
}