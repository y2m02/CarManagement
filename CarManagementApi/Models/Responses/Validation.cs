using System.Collections.Generic;

namespace CarManagementApi.Models.Responses
{
    public class Validation : IResponse
    {
        public Validation(IEnumerable<string> errors) => Errors = errors;

        public IEnumerable<string> Errors { get; }
    }
}
