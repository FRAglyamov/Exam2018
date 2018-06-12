using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Module2.Data.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileModelId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FileModelId",
                table: "AspNetUsers",
                column: "FileModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Files_FileModelId",
                table: "AspNetUsers",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Files_FileModelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FileModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FileModelId",
                table: "AspNetUsers");
        }
    }
}
