using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addMigrationCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e27fa0c-082a-455f-9e69-c204afd7c1b3", "AQAAAAIAAYagAAAAEADcXcnyaRLfFKIYy/28zkF8y+nTOtHPqvNbwK8No2jb2h9IivGPPrpLdWYuO5yNNA==", "de54ae81-b273-46be-84b7-836d656fd526" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a139aac5-8104-4408-bbed-21e3c1b3c814", "AQAAAAIAAYagAAAAEGcAF/I8zM5IEzieDS29hW+pPF7C0o3qX+TtQ6RQu4muMgOD5c4EDuupDKZFr5oCKA==", "5a888442-96b2-451c-8e08-f4eea08821a4" });
        }
    }
}
