using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarManagementApi.Models.Results;

namespace CarManagementApi.Helpers
{
    public static class ResultHandler
    {
        public static Success Success() => new();

        public static Success Success<T>(T response) where T : class
        {
            return new(response);
        }

        public static Validation Validations(IEnumerable<string> errors) => new(errors);

        public static NotFound NotFound() => new();

        public static Unauthorized Unauthorized(string error) => new(new[] { error });

        public static Unauthorized Unauthorized(IEnumerable<string> errors) => new(errors);

        public static Failure Failure(string error) => new(new[] { error });

        public static Failure Failure(IEnumerable<string> errors) => new(errors);

        //public static async Task<IResponse> HandleErrors<TReturn>(Func<Task<TReturn>> executor) where TReturn : class
        //{
        //    try
        //    {
        //        return Success(await executor().ConfigureAwait(false));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Failure(new[] { ex.Message });
        //    }
        //}

        public static async Task<IResult> HandleErrors(Func<Task<IResult>> executor)
        {
            try
            {
                return await executor().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return Failure(new[] { ex.Message });
            }
        }

        public static async Task<IResult> HandleErrors(Func<Task> executor)
        {
            try
            {
                await executor().ConfigureAwait(false);

                return Success();
            }
            catch (Exception ex)
            {
                return Failure(new[] { ex.Message });
            }
        }
    }
}
