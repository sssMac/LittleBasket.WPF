using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LittleBasket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Date = table.Column<long>(type: "bigint", nullable: false),
                    Sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    IsVisible = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProductName = table.Column<string>(type: "longtext", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checks", x => new { x.HistoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Checks_Histories_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "IsVisible", "Name" },
                values: new object[,]
                {
                    { new Guid("0dce9faf-322d-4d86-b737-70515b3aff22"), false, "Хлеб" },
                    { new Guid("0e6603ad-9296-4f3e-8fcb-bce88c58871f"), false, "Молоко" },
                    { new Guid("1355dab6-9150-439a-a7dd-771c00b108ef"), false, "Арбуз" },
                    { new Guid("19d2459c-85f4-49b2-b251-c8ce88437ff6"), false, "Мясо" },
                    { new Guid("2c1f9af4-6eeb-404f-b148-984d8faa6f48"), false, "Масло" },
                    { new Guid("4083bbc6-bad7-41dd-84b9-be5f9a72a376"), false, "Колбаса" },
                    { new Guid("85e7ec78-dc31-476c-9d39-da8df51e7935"), false, "Сыр" },
                    { new Guid("f2e8609e-4ee3-4bbd-9e89-b46f51732a8a"), false, "Печенье" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Histories");
        }
    }
}
