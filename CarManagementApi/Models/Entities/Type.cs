using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarManagementApi.Models.Entities
{
    public class Type : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
