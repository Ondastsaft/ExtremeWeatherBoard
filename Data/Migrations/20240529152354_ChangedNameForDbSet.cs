using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNameForDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Threads_CommentThreadId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_AdminUserDatas_CreatorUserId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_SubCategories_SubCategoryId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_UserDatas_UserDataId",
                table: "Threads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Threads",
                table: "Threads");

            migrationBuilder.RenameTable(
                name: "Threads",
                newName: "DiscussionThreads");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_UserDataId",
                table: "DiscussionThreads",
                newName: "IX_DiscussionThreads_UserDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_SubCategoryId",
                table: "DiscussionThreads",
                newName: "IX_DiscussionThreads_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_CreatorUserId",
                table: "DiscussionThreads",
                newName: "IX_DiscussionThreads_CreatorUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscussionThreads",
                table: "DiscussionThreads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_DiscussionThreads_CommentThreadId",
                table: "Comments",
                column: "CommentThreadId",
                principalTable: "DiscussionThreads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionThreads_AdminUserDatas_CreatorUserId",
                table: "DiscussionThreads",
                column: "CreatorUserId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionThreads_SubCategories_SubCategoryId",
                table: "DiscussionThreads",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionThreads_UserDatas_UserDataId",
                table: "DiscussionThreads",
                column: "UserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_DiscussionThreads_CommentThreadId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionThreads_AdminUserDatas_CreatorUserId",
                table: "DiscussionThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionThreads_SubCategories_SubCategoryId",
                table: "DiscussionThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionThreads_UserDatas_UserDataId",
                table: "DiscussionThreads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscussionThreads",
                table: "DiscussionThreads");

            migrationBuilder.RenameTable(
                name: "DiscussionThreads",
                newName: "Threads");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionThreads_UserDataId",
                table: "Threads",
                newName: "IX_Threads_UserDataId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionThreads_SubCategoryId",
                table: "Threads",
                newName: "IX_Threads_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionThreads_CreatorUserId",
                table: "Threads",
                newName: "IX_Threads_CreatorUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Threads",
                table: "Threads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Threads_CommentThreadId",
                table: "Comments",
                column: "CommentThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_AdminUserDatas_CreatorUserId",
                table: "Threads",
                column: "CreatorUserId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_SubCategories_SubCategoryId",
                table: "Threads",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_UserDatas_UserDataId",
                table: "Threads",
                column: "UserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");
        }
    }
}
