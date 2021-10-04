using AutoMapper;
using CarManagementApi.Models.Dtos;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Requests;
using CarManagementApi.Repositories;

namespace CarManagementApi.Services
{
    public interface IBrandService : IService<BrandRequest> { }

    public class BrandService : BaseService<Brand, BrandDto, BrandRequest>, IBrandService
    {
        public BrandService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : base(unitOfWork, mapper) { }

        protected override void Update(Brand entity, BrandRequest request)
        {
            entity.Name = request.Name;
        }
    }
}
