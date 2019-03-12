using System.Collections.Generic;

namespace BackEnd_zadatak.Dtos
{
    public class DeviceToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DeviceType { get; set; }
        public int DeviceTypeId { get; set; }
        public decimal Price { get; set; }
        public ICollection<DevicePropertyToReturnDto> DevicePropertyValues { get; set; }

    }
}