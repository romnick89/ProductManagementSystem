using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultRolesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17bb7d8a-885b-4df9-9c9e-b1ea79b056aa", null, "Administrator", "ADMINISTRATOR" },
                    { "68dd38db-50d7-4480-90c0-29913a649f77", null, "Supervisor", "SUPERVISOR" },
                    { "6f42fdf1-b675-4b8f-8a04-e8aa6d405fc7", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ffc16461-f454-4d61-af1b-86252f0a2703", 0, "7eebddb1-05c7-4b32-bd86-44a76f9743a2", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEAI5dUN7uWU/UE+htgo9vWlvSxcrwrY5SM4cfYebczjEagSE3Xm7kd4238M2bYKw6w==", null, false, "22161994-56f5-471c-871e-79251d2bb702", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "17bb7d8a-885b-4df9-9c9e-b1ea79b056aa", "ffc16461-f454-4d61-af1b-86252f0a2703" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68dd38db-50d7-4480-90c0-29913a649f77");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f42fdf1-b675-4b8f-8a04-e8aa6d405fc7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "17bb7d8a-885b-4df9-9c9e-b1ea79b056aa", "ffc16461-f454-4d61-af1b-86252f0a2703" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17bb7d8a-885b-4df9-9c9e-b1ea79b056aa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703");
        }
    }
}
