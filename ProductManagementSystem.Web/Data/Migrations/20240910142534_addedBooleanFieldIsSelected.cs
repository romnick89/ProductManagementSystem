using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedBooleanFieldIsSelected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a139aac5-8104-4408-bbed-21e3c1b3c814", "AQAAAAIAAYagAAAAEGcAF/I8zM5IEzieDS29hW+pPF7C0o3qX+TtQ6RQu4muMgOD5c4EDuupDKZFr5oCKA==", "5a888442-96b2-451c-8e08-f4eea08821a4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93d3759b-88db-45da-bc7e-9c2ec8912413", "AQAAAAIAAYagAAAAECeohPO0sl+3rRbqpzSsTO2oI2b3c0njqmU8mKyqyYXGfxtCE314Fe1aoUNvWRTxEQ==", "ad4662a2-cfce-4582-b86e-d98ecaba898b" });
        }
    }
}
