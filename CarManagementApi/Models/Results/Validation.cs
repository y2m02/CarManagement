using System.Collections.Generic;

namespace CarManagementApi.Models.Results
{
    public class Validation : IResult
    {
        public Validation(IEnumerable<string> errors) => Errors = errors;

        public IEnumerable<string> Errors { get; }
    }
}
