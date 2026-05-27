using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jasmine.FastenerDepartment.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Description", "Value" },
                values: new object[,]
                {
                    { "Company:Subtitle", "Company sub-title", "" },
                    { "Company:Title", "Company's title", "" },
                    { "Emails:DisplayName", "Name which will be shown instead of email address", "" },
                    { "Emails:Password", "Password of account for external services", "" },
                    { "Emails:SmtpPort", "Simple mail transfer protocol port", "" },
                    { "Emails:SmtpUrl", "Simple mail transfer protocol URL", "" },
                    { "Emails:UserName", "User name of an account", "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "Company:Subtitle");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "Company:Title");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "Emails:DisplayName");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "Emails:Password");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "Emails:SmtpPort");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "Emails:SmtpUrl");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: "Emails:UserName");
        }
    }
}
