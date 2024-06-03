using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeparateUserModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminLogs_UserDatas_LogsAdminUserDataId",
                table: "AdminLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_UserDatas_CreatorId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_UserDatas_CreatorId",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_UserDatas_CreatorUserId",
                table: "DiscussionThreads");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "UserDatas");

            migrationBuilder.AddColumn<int>(
                name: "UserDataId",
                table: "DiscussionThreads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminReceiverId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminSenderId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentAdminUserDataId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdminUserDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUserDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminUserDatas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Threads_UserDataId",
                table: "DiscussionThreads",
                column: "UserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AdminSenderId",
                table: "Messages",
                column: "AdminSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentAdminUserDataId",
                table: "Comments",
                column: "CommentAdminUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserDatas_UserId",
                table: "AdminUserDatas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminLogs_AdminUserDatas_LogsAdminUserDataId",
                table: "AdminLogs",
                column: "LogsAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AdminUserDatas_CreatorId",
                table: "Categories",
                column: "DiscussionThreadAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AdminUserDatas_CommentAdminUserDataId",
                table: "Comments",
                column: "CommentAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AdminUserDatas_AdminSenderId",
                table: "Messages",
                column: "AdminSenderId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AdminUserDatas_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_AdminUserDatas_CreatorId",
                table: "SubCategories",
                column: "DiscussionThreadAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_AdminUserDatas_CreatorUserId",
                table: "DiscussionThreads",
                column: "DiscussionThreadAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_UserDatas_UserDataId",
                table: "DiscussionThreads",
                column: "UserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminLogs_AdminUserDatas_LogsAdminUserDataId",
                table: "AdminLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AdminUserDatas_CreatorId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AdminUserDatas_CommentAdminUserDataId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AdminUserDatas_AdminSenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AdminUserDatas_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_AdminUserDatas_CreatorId",
                table: "SubCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_AdminUserDatas_CreatorUserId",
                table: "DiscussionThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_UserDatas_UserDataId",
                table: "DiscussionThreads");

            migrationBuilder.DropTable(
                name: "AdminUserDatas");

            migrationBuilder.DropIndex(
                name: "IX_Threads_UserDataId",
                table: "DiscussionThreads");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AdminSenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentAdminUserDataId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "DiscussionThreads");

            migrationBuilder.DropColumn(
                name: "AdminReceiverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AdminSenderId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "CommentAdminUserDataId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UserDatas",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminLogs_UserDatas_LogsAdminUserDataId",
                table: "AdminLogs",
                column: "LogsAdminUserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_UserDatas_CreatorId",
                table: "Categories",
                column: "DiscussionThreadAdminUserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_UserDatas_CreatorId",
                table: "SubCategories",
                column: "DiscussionThreadAdminUserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_UserDatas_CreatorUserId",
                table: "DiscussionThreads",
                column: "DiscussionThreadAdminUserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");
        }
    }
}
