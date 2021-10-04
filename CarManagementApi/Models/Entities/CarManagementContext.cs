using Microsoft.EntityFrameworkCore;

namespace CarManagementApi.Models.Entities
{
    public class CarManagementContext : DbContext
    {
        public CarManagementContext(DbContextOptions options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
