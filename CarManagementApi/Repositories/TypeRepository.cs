using CarManagementApi.Models.Entities;

namespace CarManagementApi.Repositories
{
    public interface ITypeRepository : IRepository<Type> { }

    public class TypeRepository : Repository<Type>, ITypeRepository
    {
        public TypeRepository(CarManagementContext context) : base(context) { }
    }
}
