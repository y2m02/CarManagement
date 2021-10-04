using CarManagementApi.Models.Entities;

namespace CarManagementApi.Repositories
{
    public interface ITypeRepository : IRepository<Type> { }

    public class TypeRepository : BaseRepository<Type>, ITypeRepository
    {
        public TypeRepository(CarManagementContext context) : base(context) { }
    }
}
