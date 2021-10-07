using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarManagementApi.Migrations
{
    public partial class AdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //var id = Guid.NewGuid().ToString();

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            //    values: new object[]
            //    {
            //        ,
            //        "Admin",
            //        "ADMIN",
            //        DateTime.Now.ToString()
            //    }
            //);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
