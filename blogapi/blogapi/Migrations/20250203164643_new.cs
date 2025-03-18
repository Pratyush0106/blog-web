using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogapi.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Likes",
                table: "Posts",
                newName: "AuthorID");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorID",
                table: "Posts",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AuthorID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Posts",
                newName: "Likes");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
