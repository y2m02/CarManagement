using System.Collections.Generic;

namespace CarManagementApi.Models.Requests
{
    public class AddAppUserToRolesRequest
    {
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
