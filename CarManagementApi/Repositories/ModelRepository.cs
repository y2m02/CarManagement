using System.Collections.Generic;
using System.Threading.Tasks;
using CarManagementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarManagementApi.Repositories
{
    public interface IModelRepository : IRepository<Model> { }

    public class ModelRepository : BaseRepository<Model>, IModelRepository
    {
        public ModelRepository(CarManagementContext context) : base(context) { }

        public override Task<List<Model>> GetAll(int pageNumber, int pageSize)
        {
            return GetEntities(pageNumber, pageSize)
                .Include(x => x.Brand)
                .ToListAsync();
        }
    }
}
