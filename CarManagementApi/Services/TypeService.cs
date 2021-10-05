using AutoMapper;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Responses;
using CarManagementApi.Repositories;

namespace CarManagementApi.Services
{
    public interface ITypeService : IService<TypeRequest> { }

    public class TypeService : BaseService<Type, TypeResponse, TypeRequest>, ITypeService
    {
        public TypeService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : base(unitOfWork, mapper) { }

        protected override void Update(Type entity, TypeRequest request)
        {
            entity.Description = request.Description;
        }
    }
}
