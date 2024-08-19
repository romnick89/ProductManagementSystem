using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1060116-10de-4a72-977f-1f1ffeb1c1ac", "AQAAAAIAAYagAAAAEIN/d66scF6lQzNiIMt7Y6sdHRN/XoA+hg7a8Zsjh0BUWnOl+qDv4Q7RTLGjm7tBDA==", "08a8ee38-72d6-47d4-a310-c2fd6e82693d" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04c6375d-5b45-4589-b436-e03141d4f695", "AQAAAAIAAYagAAAAEPB1B7nfb/kQZ6f6QIPZ+83BNEo+AwTnWrfNujrHdi/ukpfqjO+QHvXSzxutGb8sJQ==", "ea3d0f91-a0f8-4c74-a326-25fea8947680" });
        }
    }
}
