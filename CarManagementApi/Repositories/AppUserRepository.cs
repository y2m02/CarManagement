using System.Collections.Generic;
using System.Threading.Tasks;
using CarManagementApi.Models.Entities;
using HelpersLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarManagementApi.Repositories
{
    public interface IAppUserRepository
    {
        Task<List<AppUser>> GetAll();
        Task<IdentityResult> Add(AppUser user, string password);
        Task<IdentityResult> AddToRole(AppUser user, string role);
        Task<IdentityResult> AddToRoles(AppUser user, IEnumerable<string> roles);
        Task<List<string>> GetRoles(string userName);
    }

    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> userManager;

        public AppUserRepository(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public Task<List<AppUser>> GetAll()
        {
            return userManager.Users.ToListAsync();
        }

        public Task<IdentityResult> Add(AppUser user, string password)
        {
            return userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> AddToRole(AppUser user, string role)
        {
            return userManager.AddToRoleAsync(user, role);
        }

        public Task<IdentityResult> AddToRoles(AppUser user, IEnumerable<string> roles)
        {
            return userManager.AddToRolesAsync(user, roles);
        }

        public async Task<List<string>> GetRoles(string userName)
        {
            var user = await userManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleAsync(u => u.UserName == userName);

            return user.UserRoles.SelectListOf(ur => ur.Role.Name);
        }
    }
}
