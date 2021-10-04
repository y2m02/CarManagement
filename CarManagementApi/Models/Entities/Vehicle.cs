using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarManagementApi.Models.Entities
{
    public class Vehicle : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public int ModelId { get; set; }

        public int TypeId { get; set; }

        [ForeignKey(nameof(ModelId))]
        public Model Model { get; set; }

        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; }
    }
}
