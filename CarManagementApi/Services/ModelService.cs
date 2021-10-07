using AutoMapper;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Responses;
using CarManagementApi.Repositories;

namespace CarManagementApi.Services
{
    public interface IModelService : IService<ModelRequest> { }

    public class ModelService : Service<Model, ModelResponse, ModelRequest>, IModelService
    {
        public ModelService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : base(unitOfWork, mapper) { }

        protected override void Update(Model entity, ModelRequest request)
        {
            entity.Name = request.Name;
            entity.BrandId = request.BrandId;
        }
    }
}
