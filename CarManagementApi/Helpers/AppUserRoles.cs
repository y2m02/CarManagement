namespace CarManagementApi.Helpers
{
    public static class AppUserRoles
    {
        public const string Admin = "Admin";
        public const string CanRead = "CanRead";
        public const string CanAdd = "CanAdd";
        public const string CanUpdate = "CanUpdate";
        public const string CanDelete = "CanDelete";

        public const string AdminOrCanRead = "Admin,CanRead";
        public const string AdminOrCanAdd = "Admin,CanAdd";
        public const string AdminOrCanUpdate = "Admin,CanUpdate";
        public const string AdminOrCanDelete = "Admin,CanDelete";
    }
}
