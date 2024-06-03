using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class CommentDiscussionThreadLinkFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_DiscussionThreads_CommentThreadId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_DiscussionThreads_CommentThreadId",
                table: "Comments",
                column: "CommentThreadId",
                principalTable: "DiscussionThreads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_DiscussionThreads_CommentThreadId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_DiscussionThreads_CommentThreadId",
                table: "Comments",
                column: "CommentThreadId",
                principalTable: "DiscussionThreads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
