using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jasmine.FastenerDepartment.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "varchar(500)", nullable: true),
                    TypeCode = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_TemplateTypes_TypeCode",
                        column: x => x.TypeCode,
                        principalTable: "TemplateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TemplateTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "{\"En\":\"Product catalog\",\"Ru\":\"Каталог товаров\"}" },
                    { 2, "{\"En\":\"Order form\",\"Ru\":\"Бланк заказа\"}" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Templates_Title",
                table: "Templates",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TypeCode",
                table: "Templates",
                column: "TypeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "TemplateTypes");
        }
    }
}
