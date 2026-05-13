using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.DAL.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserUrlRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ShortenUrls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ShortenUrls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShortenUrls_UserId",
                table: "ShortenUrls",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShortenUrls_Users_UserId",
                table: "ShortenUrls",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortenUrls_Users_UserId",
                table: "ShortenUrls");

            migrationBuilder.DropIndex(
                name: "IX_ShortenUrls_UserId",
                table: "ShortenUrls");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ShortenUrls");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShortenUrls");
        }
    }
}
