using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_zadatak.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required]
        public string Name { get; set; }
        public decimal? Price { get; set; }
        
        public int? DeviceTypeId { get; set; }
        [ForeignKey("DeviceTypeId")]
        public DeviceType DeviceType { get; set; }
        public ICollection<DevicePropertyValue> DevicePropertyValues { get; set; }
    }
}