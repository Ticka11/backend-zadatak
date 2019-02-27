namespace BackEnd_zadatak.Dtos
{
    public class DevicePropertyForCreationOrUpdate
    {
        public int? Id { get; set; } 
        public int? DeviceTypePropertyId  { get; set; }
        public int? DeviceId { get; set; }
        public string Value { get; set; }
    }
}