﻿using AutoMapper;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Responses;
using CarManagementApi.Repositories;

namespace CarManagementApi.Services
{
    public interface IBrandService : IService<BrandRequest> { }

    public class BrandService : Service<Brand, BrandResponse, BrandRequest>, IBrandService
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
