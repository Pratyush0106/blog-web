using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogapi.Migrations
{
    /// <inheritdoc />
    public partial class Again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorID",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Posts",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorID",
                table: "Posts",
                newName: "IX_Posts_AuthorId");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "Posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Posts",
                newName: "AuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                newName: "IX_Posts_AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorID",
                table: "Posts",
                column: "AuthorID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
