using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarManagementApi.Helpers;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Responses;
using CarManagementApi.Models.Results;
using CarManagementApi.Repositories;

namespace CarManagementApi.Services
{
    public interface IService<in TRequest> where TRequest : class
    {
        Task<IResult> GetAll(int pageNumber, int pageSize);
        Task<IResult> GetById(int id);
        Task<IResult> Add(TRequest request);
        Task<IResult> Update(int id, TRequest request);
        Task<IResult> Remove(int id);
    }

    public abstract class BaseService<TEntity, TResponse, TRequest>
        where TResponse : BaseResponse
        where TRequest : class
        where TEntity : BaseEntity, new()

    {
        protected readonly IMapper mapper;
        protected readonly IUnitOfWork unitOfWork;
        private readonly BaseRepository<TEntity> repository;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

            repository = unitOfWork.Entity<TEntity>();
        }

        public Task<IResult> GetAll(int pageNumber, int pageSize)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var response = await repository
                        .GetAll(pageNumber, pageSize)
                        .ConfigureAwait(false);

                    return ResultHandler.Success(mapper.Map<List<TResponse>>(response));
                }
            );
        }

        public Task<IResult> GetById(int id)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var response = await repository.GetById(id).ConfigureAwait(false);

                    return response is null
                        ? ResultHandler.NotFound()
                        : ResultHandler.Success(mapper.Map<TResponse>(response));
                }
            );
        }

        public Task<IResult> Add(TRequest request)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var mappedEntity = mapper.Map<TEntity>(request);

                    await repository.Add(mappedEntity).ConfigureAwait(false);
                    await unitOfWork.Complete().ConfigureAwait(false);

                    var entity = await repository
                        .GetById(mappedEntity.Id)
                        .ConfigureAwait(false);

                    return ResultHandler.Success(mapper.Map<TResponse>(entity));
                }
            );
        }

        public Task<IResult> Update(int id, TRequest request)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var entity = await repository.GetById(id).ConfigureAwait(false);

                    if (entity is null)
                    {
                        return ResultHandler.NotFound();
                    }

                    Update(entity, request);

                    await unitOfWork.Complete().ConfigureAwait(false);

                    return ResultHandler.Success(mapper.Map<TResponse>(entity));
                }
            );
        }

        public Task<IResult> Remove(int id)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    await repository.Remove(id).ConfigureAwait(false);

                    await unitOfWork.Complete().ConfigureAwait(false);

                    return ResultHandler.Success(new { id });
                }
            );
        }

        protected abstract void Update(TEntity entity, TRequest request);
    }
}
