using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Africuisine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AuthContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ROLES",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9322247b-70ef-4bad-a57a-3b6cf1604570", "3f883c78-c6c8-43d5-be6e-a621835fcb8c", "Mobile", "MOBILE" },
                    { "b1410ddd-d4c9-44a6-aeb2-bd00ac2a3a03", "1d60d4a5-b2b7-44b3-b978-4b806147523b", "Super", "SUPER" },
                    { "d65f9a81-ad4a-4669-93df-c00f7b653885", "55b0454b-3e8b-4e78-8e1b-445a4a93db52", "Restricted", "RESTRICTED" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "Id",
                keyValue: "9322247b-70ef-4bad-a57a-3b6cf1604570");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "Id",
                keyValue: "b1410ddd-d4c9-44a6-aeb2-bd00ac2a3a03");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "Id",
                keyValue: "d65f9a81-ad4a-4669-93df-c00f7b653885");
        }
    }
}
