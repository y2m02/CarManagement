using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarManagementApi.Models.Entities
{
    public class CarManagementContext :
        IdentityDbContext<
            AppUser,
            AppRole,
            string,
            IdentityUserClaim<string>,
            AppUserRole,
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>,
            IdentityUserToken<string>
        >
    {
        public CarManagementContext(DbContextOptions options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserRole>(
                userRole =>
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                    userRole
                        .HasOne(ur => ur.Role)
                        .WithMany(ur => ur.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                    userRole
                        .HasOne(ur => ur.User)
                        .WithMany(ur => ur.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
                }
            );
        }
    }
}
