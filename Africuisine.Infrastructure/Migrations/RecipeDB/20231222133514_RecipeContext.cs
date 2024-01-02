using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Africuisine.Infrastructure.Migrations.RecipeDB
{
    /// <inheritdoc />
    public partial class RecipeContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "PICTURES",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeqNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PICTURES", x => x.Link);
                    table.ForeignKey(
                        name: "FK_PICTURES_USERS_LUserUpdate",
                        column: x => x.LUserUpdate,
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RECCATEGORIES",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeqNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECCATEGORIES", x => x.Link);
                    table.ForeignKey(
                        name: "FK_RECCATEGORIES_USERS_LUserUpdate",
                        column: x => x.LUserUpdate,
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RECDIFFICULTY",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeqNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECDIFFICULTY", x => x.Link);
                    table.ForeignKey(
                        name: "FK_RECDIFFICULTY_USERS_LUserUpdate",
                        column: x => x.LUserUpdate,
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROFILEPICTURES",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false),
                    LPicture = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "RECIPES",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LCategory = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LDifficulty = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeqNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECIPES", x => x.Link);
                    table.ForeignKey(
                        name: "FK_RECIPES_RECCATEGORIES_LCategory",
                        column: x => x.LCategory,
                        principalTable: "RECCATEGORIES",
                        principalColumn: "Link");
                    table.ForeignKey(
                        name: "FK_RECIPES_RECDIFFICULTY_LDifficulty",
                        column: x => x.LDifficulty,
                        principalTable: "RECDIFFICULTY",
                        principalColumn: "Link");
                    table.ForeignKey(
                        name: "FK_RECIPES_USERS_LUserUpdate",
                        column: x => x.LUserUpdate,
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RECINSTRUCTIONS",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LRecipe = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeqNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECINSTRUCTIONS", x => x.Link);
                    table.ForeignKey(
                        name: "FK_RECINSTRUCTIONS_RECIPES_LRecipe",
                        column: x => x.LRecipe,
                        principalTable: "RECIPES",
                        principalColumn: "Link");
                    table.ForeignKey(
                        name: "FK_RECINSTRUCTIONS_USERS_LUserUpdate",
                        column: x => x.LUserUpdate,
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RECPICTURES",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LRecipe = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LPicture = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeqNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECPICTURES", x => x.Link);
                    table.ForeignKey(
                        name: "FK_RECPICTURES_PICTURES_LPicture",
                        column: x => x.LPicture,
                        principalTable: "PICTURES",
                        principalColumn: "Link");
                    table.ForeignKey(
                        name: "FK_RECPICTURES_RECIPES_LRecipe",
                        column: x => x.LRecipe,
                        principalTable: "RECIPES",
                        principalColumn: "Link");
                    table.ForeignKey(
                        name: "FK_RECPICTURES_USERS_LUserUpdate",
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
                name: "IX_PICTURES_LUserUpdate",
                table: "PICTURES",
                column: "LUserUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_PICTURES_Name",
                table: "PICTURES",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PROFILEPICTURES_LPicture",
                table: "PROFILEPICTURES",
                column: "LPicture");

            migrationBuilder.CreateIndex(
                name: "IX_PROFILEPICTURES_LUserUpdate",
                table: "PROFILEPICTURES",
                column: "LUserUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_RECCATEGORIES_LUserUpdate",
                table: "RECCATEGORIES",
                column: "LUserUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_RECDIFFICULTY_LUserUpdate",
                table: "RECDIFFICULTY",
                column: "LUserUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_RecDifficulty_Name",
                table: "RECDIFFICULTY",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RECINSTRUCTIONS_LRecipe",
                table: "RECINSTRUCTIONS",
                column: "LRecipe");

            migrationBuilder.CreateIndex(
                name: "IX_RECINSTRUCTIONS_LUserUpdate",
                table: "RECINSTRUCTIONS",
                column: "LUserUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_RECIPES_LCategory",
                table: "RECIPES",
                column: "LCategory");

            migrationBuilder.CreateIndex(
                name: "IX_RECIPES_LDifficulty",
                table: "RECIPES",
                column: "LDifficulty");

            migrationBuilder.CreateIndex(
                name: "IX_RECIPES_LUserUpdate",
                table: "RECIPES",
                column: "LUserUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_RECPICTURES_LPicture",
                table: "RECPICTURES",
                column: "LPicture");

            migrationBuilder.CreateIndex(
                name: "IX_RECPICTURES_LRecipe",
                table: "RECPICTURES",
                column: "LRecipe");

            migrationBuilder.CreateIndex(
                name: "IX_RECPICTURES_LUserUpdate",
                table: "RECPICTURES",
                column: "LUserUpdate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROFILEPICTURES");

            migrationBuilder.DropTable(
                name: "RECINSTRUCTIONS");

            migrationBuilder.DropTable(
                name: "RECPICTURES");

            migrationBuilder.DropTable(
                name: "PICTURES");

            migrationBuilder.DropTable(
                name: "RECIPES");

            migrationBuilder.DropTable(
                name: "RECCATEGORIES");

            migrationBuilder.DropTable(
                name: "RECDIFFICULTY");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
