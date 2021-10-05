using System.Collections.Generic;

namespace CarManagementApi.Models.Results
{
    public class Unauthorized: IResult
    {
        public Unauthorized() { }

        public Unauthorized(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}
