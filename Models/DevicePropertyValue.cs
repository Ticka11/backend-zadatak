using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_zadatak.Models
{
    public class DevicePropertyValue
    {
        [Key]
        public int Id { get; set; }
        
        public int? DeviceTypePropertyId  { get; set; }
        [ForeignKey("DeviceTypePropertyId")]
        public DeviceTypeProperty DeviceTypeProperty { get; set; }

        public int? DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
        
        [Column(TypeName = "nvarchar(max)")]
        [Required]
        public string Value { get; set; }
    }
}