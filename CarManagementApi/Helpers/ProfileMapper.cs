using AutoMapper;
using CarManagementApi.Models.Dtos;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Requests;

namespace CarManagementApi.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandRequest, Brand>();
        }
    }
}
