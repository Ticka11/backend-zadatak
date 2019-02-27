using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_zadatak.Models
{
    public class DeviceType
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required]
        public string Name { get; set; }
        
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual DeviceType ParentDeviceType { get; set; }
        public virtual ICollection<DeviceType> ChildrenDeviceType { get; set; }
        public ICollection<DeviceTypeProperty> DeviceTypeProperty { get; set; }
    }
}