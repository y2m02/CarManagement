using System.Collections.Generic;
using System.Threading.Tasks;
using CarManagementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarManagementApi.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle> { }

    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(CarManagementContext context) : base(context) { }

        public override Task<List<Vehicle>> GetAll(int pageNumber, int pageSize)
        {
            return GetEntities(pageNumber, pageSize)
                .Include(x => x.Model)
                .Include(x => x.Type)
                .ToListAsync();
        }
    }
}
