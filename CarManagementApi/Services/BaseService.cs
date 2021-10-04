using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarManagementApi.Helpers;
using CarManagementApi.Models.Dtos;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Responses;
using CarManagementApi.Repositories;

namespace CarManagementApi.Services
{
    public interface IService<in TRequest> where TRequest : class
    {
        Task<IResponse> GetAll(int pageNumber, int pageSize);
        Task<IResponse> GetById(int id);
        Task<IResponse> Add(TRequest request);
        Task<IResponse> Update(int id, TRequest request);
        Task<IResponse> Remove(int id);
    }

    public abstract class BaseService<TEntity, TDto, TRequest>
        where TDto : BaseDto
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

        public Task<IResponse> GetAll(int pageNumber, int pageSize)
        {
            return ResponseHandler.HandleErrors(
                async () =>
                {
                    var response = await repository
                        .GetAll(pageNumber, pageSize)
                        .ConfigureAwait(false);

                    return mapper.Map<List<TDto>>(response);
                }
            );
        }

        public Task<IResponse> GetById(int id)
        {
            return ResponseHandler.HandleErrors(
                async () =>
                {
                    var response = await repository.GetById(id).ConfigureAwait(false);

                    return mapper.Map<TDto>(response);
                }
            );
        }

        public Task<IResponse> Add(TRequest request)
        {
            return ResponseHandler.HandleErrors(
                async () =>
                {
                    var mappedEntity = mapper.Map<TEntity>(request);

                    await repository.Add(mappedEntity).ConfigureAwait(false);
                    await unitOfWork.Complete().ConfigureAwait(false);

                    var entity = await repository
                        .GetById(mappedEntity.Id)
                        .ConfigureAwait(false);

                    return mapper.Map<TDto>(entity);
                }
            );
        }

        public Task<IResponse> Update(int id, TRequest request)
        {
            return ResponseHandler.HandleErrors(
                async () =>
                {
                    var entity = await repository.GetById(id).ConfigureAwait(false);

                    Update(entity, request);

                    await unitOfWork.Complete().ConfigureAwait(false);

                    return mapper.Map<TDto>(entity);
                }
            );
        }

        public Task<IResponse> Remove(int id)
        {
            return ResponseHandler.HandleErrors(
                async () =>
                {
                    await repository.Remove(id).ConfigureAwait(false);

                    await unitOfWork.Complete().ConfigureAwait(false);

                    return ResponseHandler.Success(new { id });
                }
            );
        }

        protected abstract void Update(TEntity entity, TRequest request);
    }
}
