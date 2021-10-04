using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarManagementApi.Models.Entities
{
    public class Brand : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }
    }
}
