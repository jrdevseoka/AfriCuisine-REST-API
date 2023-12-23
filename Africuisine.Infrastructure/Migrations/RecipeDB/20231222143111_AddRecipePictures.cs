using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Africuisine.Infrastructure.Migrations.RecipeDB
{
    /// <inheritdoc />
    public partial class AddRecipePictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROFILEPICTURES");

            migrationBuilder.DeleteData(
                table: "RECDIFFICULTY",
                keyColumn: "Link",
                keyValue: "1a061260-7870-4cd0-a4b3-cc786e749196");

            migrationBuilder.DeleteData(
                table: "RECDIFFICULTY",
                keyColumn: "Link",
                keyValue: "5373f2c7-37a7-47db-9e8d-446bec6eb122");

            migrationBuilder.DeleteData(
                table: "RECDIFFICULTY",
                keyColumn: "Link",
                keyValue: "6f5d5d86-61bf-49c9-a57d-43a98714faf4");

            migrationBuilder.InsertData(
                table: "RECDIFFICULTY",
                columns: new[] { "Link", "Creation", "LUserUpdate", "Name", "SeqNo" },
                values: new object[,]
                {
                    { "6333a9a8-47ad-4d2e-aa0d-096a6deafaa3", new DateTime(2023, 12, 22, 16, 31, 11, 693, DateTimeKind.Local).AddTicks(2839), null, "Hard", 0 },
                    { "c6257fc0-769c-4e08-b3c3-75ff40a05011", new DateTime(2023, 12, 22, 16, 31, 11, 693, DateTimeKind.Local).AddTicks(2786), null, "Easy", 0 },
                    { "f35a1208-e1a2-4909-b299-373808dd101b", new DateTime(2023, 12, 22, 16, 31, 11, 693, DateTimeKind.Local).AddTicks(2843), null, "Expert", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RECDIFFICULTY",
                keyColumn: "Link",
                keyValue: "6333a9a8-47ad-4d2e-aa0d-096a6deafaa3");

            migrationBuilder.DeleteData(
                table: "RECDIFFICULTY",
                keyColumn: "Link",
                keyValue: "c6257fc0-769c-4e08-b3c3-75ff40a05011");

            migrationBuilder.DeleteData(
                table: "RECDIFFICULTY",
                keyColumn: "Link",
                keyValue: "f35a1208-e1a2-4909-b299-373808dd101b");

            migrationBuilder.CreateTable(
                name: "PROFILEPICTURES",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LPicture = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Activated = table.Column<bool>(type: "bit", nullable: false),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeqNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFILEPICTURES", x => x.Link);
                    table.ForeignKey(
                        name: "FK_PROFILEPICTURES_PICTURES_LPicture",
                        column: x => x.LPicture,
                        principalTable: "PICTURES",
                        principalColumn: "Link");
                    table.ForeignKey(
                        name: "FK_PROFILEPICTURES_USERS_LUserUpdate",
                        column: x => x.LUserUpdate,
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "RECDIFFICULTY",
                columns: new[] { "Link", "Creation", "LUserUpdate", "Name", "SeqNo" },
                values: new object[,]
                {
                    { "1a061260-7870-4cd0-a4b3-cc786e749196", new DateTime(2023, 12, 22, 15, 35, 14, 298, DateTimeKind.Local).AddTicks(9222), null, "Hard", 0 },
                    { "5373f2c7-37a7-47db-9e8d-446bec6eb122", new DateTime(2023, 12, 22, 15, 35, 14, 298, DateTimeKind.Local).AddTicks(9176), null, "Easy", 0 },
                    { "6f5d5d86-61bf-49c9-a57d-43a98714faf4", new DateTime(2023, 12, 22, 15, 35, 14, 298, DateTimeKind.Local).AddTicks(9226), null, "Expert", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROFILEPICTURES_LPicture",
                table: "PROFILEPICTURES",
                column: "LPicture");

            migrationBuilder.CreateIndex(
                name: "IX_PROFILEPICTURES_LUserUpdate",
                table: "PROFILEPICTURES",
                column: "LUserUpdate");
        }
    }
}
