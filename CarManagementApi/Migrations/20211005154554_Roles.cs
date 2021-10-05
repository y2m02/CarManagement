using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CarManagementApi.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[]
                {
                    Guid.NewGuid().ToString(), 
                    "Admin", 
                    "ADMIN", 
                    DateTime.Now.ToString()
                }
            );

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[]
                {
                    Guid.NewGuid().ToString(), 
                    "CanRead", 
                    "CANREAD", 
                    DateTime.Now.ToString()
                }
            );

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[]
                {
                    Guid.NewGuid().ToString(), 
                    "CanAdd", 
                    "CANADD", 
                    DateTime.Now.ToString()
                }
            );

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[]
                {
                    Guid.NewGuid().ToString(), 
                    "CanUpdate", 
                    "CANUPDATE", 
                    DateTime.Now.ToString()
                }
            );

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[]
                {
                    Guid.NewGuid().ToString(), 
                    "CanDelete", 
                    "CANDELETE", 
                    DateTime.Now.ToString()
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
