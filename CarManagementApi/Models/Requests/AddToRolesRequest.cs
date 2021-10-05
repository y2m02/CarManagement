using System.Collections.Generic;

namespace CarManagementApi.Models.Requests
{
    public class AddToRolesRequest
    {
        public string UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
