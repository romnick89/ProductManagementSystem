using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderListUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93d3759b-88db-45da-bc7e-9c2ec8912413", "AQAAAAIAAYagAAAAECeohPO0sl+3rRbqpzSsTO2oI2b3c0njqmU8mKyqyYXGfxtCE314Fe1aoUNvWRTxEQ==", "ad4662a2-cfce-4582-b86e-d98ecaba898b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "465290df-308a-452c-b37a-7698f7c2861f", "AQAAAAIAAYagAAAAEInEbDIsiD66srd4RHhpq0LUyRjEo1fOI1Xi9UeEr+hXXb2DPE0mxZmidVTUOn27vw==", "da5070cd-eece-4d73-a58f-b6917c5c909c" });
        }
    }
}
