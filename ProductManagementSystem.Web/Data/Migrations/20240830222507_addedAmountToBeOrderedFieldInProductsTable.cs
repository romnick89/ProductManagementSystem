using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedAmountToBeOrderedFieldInProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountToBeOrdered",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0b78b02-05f5-4667-9465-b60454b3ccdd", "AQAAAAIAAYagAAAAEH5tJ2KLsKWEOorMJ3ZMJKsBDlMTBh3JkvZPZANSGVOqb+dXGQ3KfFW1CgnKuS5MSw==", "bdb2f5ba-ca97-4da3-895b-8d2d5d23c907" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToBeOrdered",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1060116-10de-4a72-977f-1f1ffeb1c1ac", "AQAAAAIAAYagAAAAEIN/d66scF6lQzNiIMt7Y6sdHRN/XoA+hg7a8Zsjh0BUWnOl+qDv4Q7RTLGjm7tBDA==", "08a8ee38-72d6-47d4-a310-c2fd6e82693d" });
        }
    }
}
