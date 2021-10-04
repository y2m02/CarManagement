using System.ComponentModel.DataAnnotations;

namespace CarManagementApi.Models.Requests
{
    public class TypeRequest
    {
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
