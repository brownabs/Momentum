using Microsoft.EntityFrameworkCore.Migrations;

namespace Momentum.Migrations
{
    public partial class ReconstructedCommentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Comment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProjectId",
                table: "Comment",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Project_ProjectId",
                table: "Comment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Project_ProjectId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ProjectId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Comment");
        }
    }
}
