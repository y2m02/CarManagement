using System.ComponentModel.DataAnnotations;

namespace CarManagementApi.Models.Requests
{
    public class ModelRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int BrandId { get; set; }
    }
}
