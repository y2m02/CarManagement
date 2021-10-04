using System.Threading.Tasks;
using CarManagementApi.Models.Entities;

namespace CarManagementApi.Repositories
{
    public interface IUnitOfWork
    {
        IBrandRepository Brands { get; }
        ITypeRepository Types { get; }
        IModelRepository Models { get; }
        IVehicleRepository Vehicles { get; }

        BaseRepository<TEntity> Entity<TEntity>() where TEntity : BaseEntity, new();
        Task Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarManagementContext context;

        public UnitOfWork(CarManagementContext context)
        {
            this.context = context;
            Brands = new BrandRepository(context);
            Types = new TypeRepository(context);
            Models = new ModelRepository(context);
            Vehicles = new VehicleRepository(context);
        }

        public IBrandRepository Brands { get; }
        public ITypeRepository Types { get; }
        public IModelRepository Models { get; }
        public IVehicleRepository Vehicles { get; }

        public Task Complete() => context.SaveChangesAsync();

        public BaseRepository<TEntity> Entity<TEntity>() where TEntity : BaseEntity, new()
        {
            return new BaseRepository<TEntity>(context);
        }
    }
}
