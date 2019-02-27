using System.Collections.Generic;

namespace BackEnd_zadatak.Dtos
{
    public class DeviceForCreationOrUpdate
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? DeviceTypeId { get; set; }
        public ICollection<DevicePropertyForCreationOrUpdate> DevicePropertyValues { get; set; }
    }
}