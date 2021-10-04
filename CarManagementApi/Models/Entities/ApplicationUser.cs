using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CarManagementApi.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
    }
}
