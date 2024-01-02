using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Africuisine.Infrastructure.Migrations.IngredientDB
{
    /// <inheritdoc />
    public partial class AddIngredientsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "INGRCATEGORIES",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeqNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INGRCATEGORIES", x => x.Link);
                    table.ForeignKey(
                        name: "FK_INGRCATEGORIES_USERS_LUserUpdate",
                        column: x => x.LUserUpdate,
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "INGREDIENTS",
                columns: table => new
                {
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LCategory = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LUserUpdate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeqNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INGREDIENTS", x => x.Link);
                    table.ForeignKey(
                        name: "FK_INGREDIENTS_INGRCATEGORIES_LCategory",
                        column: x => x.LCategory,
                        principalTable: "INGRCATEGORIES",
                        principalColumn: "Link");
                    table.ForeignKey(
                        name: "FK_INGREDIENTS_USERS_LUserUpdate",
                        column: x => x.LUserUpdate,
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_INGRCATEGORIES_LUserUpdate",
                table: "INGRCATEGORIES",
                column: "LUserUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_INGRCATEGORIES_Name",
                table: "INGRCATEGORIES",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_INGREDIENTS_LCategory",
                table: "INGREDIENTS",
                column: "LCategory");

            migrationBuilder.CreateIndex(
                name: "IX_INGREDIENTS_LUserUpdate",
                table: "INGREDIENTS",
                column: "LUserUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_INGREDIENTS_Name",
                table: "INGREDIENTS",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INGREDIENTS");

            migrationBuilder.DropTable(
                name: "INGRCATEGORIES");
        }
    }
}
