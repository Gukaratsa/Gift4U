using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gift4U.BlazorApp.Migrations
{
    public partial class AddedGiftRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Requested = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Responded = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Started = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RequestStateId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GivenInRequestId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_GivenGifts_GivenInRequestId",
                        column: x => x.GivenInRequestId,
                        principalTable: "GivenGifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_RequestStates_RequestStateId",
                        column: x => x.RequestStateId,
                        principalTable: "RequestStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RequestStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("22da2c49-a821-4b39-b9cd-b9aff8a6e000"), "RequestApproved" });

            migrationBuilder.InsertData(
                table: "RequestStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("24429bf0-dd4a-486b-aee8-a37ae1e03921"), "Pending" });

            migrationBuilder.InsertData(
                table: "RequestStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("37399d93-2eb8-45d6-8778-717738c101d3"), "Completed" });

            migrationBuilder.InsertData(
                table: "RequestStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("e4d0782d-ff97-4783-8ce9-a262d9a7fe54"), "RequestDenied" });

            migrationBuilder.InsertData(
                table: "RequestStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("fff72d03-937d-4574-9e3a-6a183ac2cb5f"), "RequestStarted" });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_GivenInRequestId",
                table: "Requests",
                column: "GivenInRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestStateId",
                table: "Requests",
                column: "RequestStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "RequestStates");
        }
    }
}
