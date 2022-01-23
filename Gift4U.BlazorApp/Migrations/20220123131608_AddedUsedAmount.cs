﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gift4U.BlazorApp.Migrations
{
    public partial class AddedUsedAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Used",
                table: "GivenGifts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.DropColumn(
                name: "Used",
                table: "GivenGifts");
        }
    }
}
