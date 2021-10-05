using System.Collections.Generic;

namespace CarManagementApi.Models.Results
{
    public class Failure : IResult
    {
        public Failure(IEnumerable<string> errors) => Errors = errors;

        public IEnumerable<string> Errors { get; }
    }
}
