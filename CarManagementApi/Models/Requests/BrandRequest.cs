using System.ComponentModel.DataAnnotations;

namespace CarManagementApi.Models.Requests
{
    public class BrandRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
