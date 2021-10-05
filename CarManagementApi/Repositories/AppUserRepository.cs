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
        Task<IdentityResult> AddToRole(string id, string role);
        Task<IdentityResult> AddToRoles(string id, IEnumerable<string> roles);
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

        public Task<IdentityResult> AddToRole(string id, string role)
        {
            return userManager.AddToRoleAsync(new AppUser { Id = id }, role);
        }

        public Task<IdentityResult> AddToRoles(string id, IEnumerable<string> roles)
        {
            return userManager.AddToRolesAsync(new AppUser { Id = id }, roles);
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
