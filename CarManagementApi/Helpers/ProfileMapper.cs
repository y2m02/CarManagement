using AutoMapper;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Responses;

namespace CarManagementApi.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Brand, BrandResponse>();
            CreateMap<BrandRequest, Brand>();

            CreateMap<Type, TypeResponse>();
            CreateMap<TypeRequest, Type>();

            CreateMap<Model, ModelResponse>()
                .ForMember(
                    destination => destination.BrandName,
                    option => option.MapFrom(member => member.Brand.Name)
                );
            CreateMap<ModelRequest, Model>();

            CreateMap<RegisterAppUserRequest, AppUser>();
        }
    }
}
