using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CarManagementApi.Models.Entities
{
    public class AppRole : IdentityRole
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
