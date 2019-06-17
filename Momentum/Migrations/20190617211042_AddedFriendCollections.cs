using Microsoft.EntityFrameworkCore.Migrations;

namespace Momentum.Migrations
{
    public partial class AddedFriendCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_AspNetUsers_FriendId",
                table: "Friendship");

            migrationBuilder.DropIndex(
                name: "IX_Friendship_FriendId",
                table: "Friendship");

            migrationBuilder.AlterColumn<string>(
                name: "FriendId",
                table: "Friendship",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "FriendedId",
                table: "Friendship",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_FriendedId",
                table: "Friendship",
                column: "FriendedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_AspNetUsers_FriendedId",
                table: "Friendship",
                column: "FriendedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_AspNetUsers_FriendedId",
                table: "Friendship");

            migrationBuilder.DropIndex(
                name: "IX_Friendship_FriendedId",
                table: "Friendship");

            migrationBuilder.DropColumn(
                name: "FriendedId",
                table: "Friendship");

            migrationBuilder.AlterColumn<string>(
                name: "FriendId",
                table: "Friendship",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_FriendId",
                table: "Friendship",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_AspNetUsers_FriendId",
                table: "Friendship",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
