using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderListTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderList_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "465290df-308a-452c-b37a-7698f7c2861f", "AQAAAAIAAYagAAAAEInEbDIsiD66srd4RHhpq0LUyRjEo1fOI1Xi9UeEr+hXXb2DPE0mxZmidVTUOn27vw==", "da5070cd-eece-4d73-a58f-b6917c5c909c" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderList_ProductId",
                table: "OrderList",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderList");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffc16461-f454-4d61-af1b-86252f0a2703",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0b78b02-05f5-4667-9465-b60454b3ccdd", "AQAAAAIAAYagAAAAEH5tJ2KLsKWEOorMJ3ZMJKsBDlMTBh3JkvZPZANSGVOqb+dXGQ3KfFW1CgnKuS5MSw==", "bdb2f5ba-ca97-4da3-895b-8d2d5d23c907" });
        }
    }
}
