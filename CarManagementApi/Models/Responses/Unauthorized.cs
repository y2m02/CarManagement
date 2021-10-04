using System.Collections.Generic;

namespace CarManagementApi.Models.Responses
{
    public class Unauthorized: IResponse
    {
        public Unauthorized() { }

        public Unauthorized(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}
