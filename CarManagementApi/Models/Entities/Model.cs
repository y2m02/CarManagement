using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManagementApi.Models.Entities
{
    public class Model : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
