using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarManagementApi.Helpers;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Results;
using CarManagementApi.Repositories;

namespace CarManagementApi.Services
{
    public class AppUserService
    {
        private readonly IMapper mapper;
        private readonly IAppUserRepository repository;

        public AppUserService(IMapper mapper, IAppUserRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
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
                        ? ResultHandler.Success(new { user = request })
                        : ResultHandler.Validations(result.Errors.Select(e => e.Description));
                }
            );
        }
    }
}
