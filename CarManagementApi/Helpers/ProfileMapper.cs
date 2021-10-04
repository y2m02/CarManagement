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

            CreateMap<Type, TypeDto>();
            CreateMap<TypeRequest, Type>();

            CreateMap<Model, ModelDto>()
                .ForMember(
                    destination => destination.BrandName,
                    option => option.MapFrom(member => member.Brand.Name)
                );
            CreateMap<ModelRequest, Model>();
        }
    }
}
