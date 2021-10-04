using CarManagementApi.Models.Entities;

namespace CarManagementApi.Repositories
{
    public interface IBrandRepository : IRepository<Brand> { }

    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(CarManagementContext context) : base(context) { }
    }
}
