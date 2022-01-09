using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gift4U.BlazorApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GivenGifts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GiftId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GiverId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GivenGifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GivenGifts_Gifts_GiftId",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GivenGifts_Users_GiverId",
                        column: x => x.GiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GivenGifts_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GivenGifts_GiftId",
                table: "GivenGifts",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_GivenGifts_GiverId",
                table: "GivenGifts",
                column: "GiverId");

            migrationBuilder.CreateIndex(
                name: "IX_GivenGifts_ReceiverId",
                table: "GivenGifts",
                column: "ReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GivenGifts");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
