using Microsoft.EntityFrameworkCore.Migrations;

namespace Momentum.Migrations
{
    public partial class FriendshipTableCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FriendId",
                table: "Friendship",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_AspNetUsers_FriendId",
                table: "Friendship");

            migrationBuilder.DropIndex(
                name: "IX_Friendship_FriendId",
                table: "Friendship");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "Friendship");
        }
    }
}
