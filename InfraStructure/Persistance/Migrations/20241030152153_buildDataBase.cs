using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class buildDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliveryMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveryMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "productTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DelierywaysID = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_deliveryMethod_DelierywaysID",
                        column: x => x.DelierywaysID,
                        principalTable: "deliveryMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    brandId = table.Column<int>(type: "int", nullable: false),
                    typeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_productBrands_brandId",
                        column: x => x.brandId,
                        principalTable: "productBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_productTypes_typeId",
                        column: x => x.typeId,
                        principalTable: "productTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DelierywaysID",
                table: "Orders",
                column: "DelierywaysID",
                unique: true,
                filter: "[DelierywaysID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_products_brandId",
                table: "products",
                column: "brandId");

            migrationBuilder.CreateIndex(
                name: "IX_products_typeId",
                table: "products",
                column: "typeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "productBrands");

            migrationBuilder.DropTable(
                name: "productTypes");

            migrationBuilder.DropTable(
                name: "deliveryMethod");
        }
    }
}
