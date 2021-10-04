using System.Collections.Generic;

namespace CarManagementApi.Models.Responses
{
    public class Failure : IResponse
    {
        public Failure(IEnumerable<string> errors) => Errors = errors;

        public IEnumerable<string> Errors { get; }
    }
}
