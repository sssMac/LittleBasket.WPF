using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LittleBasket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitMigrate : Migration
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
                    { new Guid("075c8879-2da9-4be8-9e1b-14ca3899c3db"), true, "Хлеб" },
                    { new Guid("2c97c485-d08c-4284-8272-0560c1195c94"), true, "Масло" },
                    { new Guid("4bad619f-6424-4b52-9803-0bddbb511606"), true, "Молоко" },
                    { new Guid("69d4ed6d-6e84-4516-8313-362c5a383622"), false, "Печенье" },
                    { new Guid("7084ff33-0f61-4ecb-9002-09cba3099bdc"), false, "Сыр" },
                    { new Guid("8ed3f4a4-97d1-438c-adbf-ab3d67596be5"), false, "Колбаса" },
                    { new Guid("9ada0269-4ba4-48cb-9228-c15785ecd7c6"), true, "Мясо" },
                    { new Guid("b56e06a3-69ed-4376-8fb6-9b0a31f23fbd"), true, "Арбуз" }
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
