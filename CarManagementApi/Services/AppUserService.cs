using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarManagementApi.Helpers;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Results;
using CarManagementApi.Repositories;
using HelpersLibrary.Extensions;
using Microsoft.AspNetCore.Identity;

namespace CarManagementApi.Services
{
    public interface IAppUserService
    {
        Task<IResult> Register(RegisterAppUserRequest request);
        Task<IResult> SingIn(SignInRequest request);
        Task<IResult> AddToRoles(AddToRolesRequest request);
    }

    public class AppUserService : IAppUserService
    {
        private readonly IMapper mapper;
        private readonly IAppUserRepository repository;
        private readonly SignInManager<AppUser> signInManager;

        public AppUserService(
            IMapper mapper,
            IAppUserRepository repository,
            SignInManager<AppUser> signInManager
        )
        {
            this.mapper = mapper;
            this.repository = repository;
            this.signInManager = signInManager;
        }

        public Task<IResult> Register(RegisterAppUserRequest request)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var result = await repository
                        .Add(mapper.Map<AppUser>(request), request.Password)
                        .ConfigureAwait(false);

                    return result.Succeeded
                        ? ResultHandler.Success(request)
                        : ResultHandler.Validations(result.Errors.Select(e => e.Description));
                }
            );
        }

        public Task<IResult> SingIn(SignInRequest request)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var result = await signInManager
                        .PasswordSignInAsync(
                            request.UserName,
                            request.Password,
                            isPersistent: false,
                            lockoutOnFailure: false
                        )
                        .ConfigureAwait(false);

                    if (!result.Succeeded)
                    {
                        return ResultHandler.Unauthorized("Unauthorized");
                    }

                    return ResultHandler.Success();
                }
            );
        }

        public Task<IResult> AddToRoles(AddToRolesRequest request)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var result = await repository
                        .AddToRoles(request.UserId, request.Roles)
                        .ConfigureAwait(false);

                    return result.Succeeded
                        ? ResultHandler.Success($"User added to: {request.Roles.Join(",")}")
                        : ResultHandler.Validations(result.Errors.Select(e => e.Description));
                }
            );
        }
    }
}
