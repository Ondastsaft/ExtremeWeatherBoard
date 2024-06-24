using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "UserDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDatas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogsAdminUserDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminLogs_AdminUserDatas_LogsAdminUserDataId",
                        column: x => x.LogsAdminUserDataId,
                        principalTable: "AdminUserDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorAdminUserDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_AdminUserDatas_CreatorAdminUserDataId",
                        column: x => x.CreatorAdminUserDataId,
                        principalTable: "AdminUserDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true),
                    AdminSenderId = table.Column<int>(type: "int", nullable: true),
                    AdminReceiverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AdminUserDatas_AdminReceiverId",
                        column: x => x.AdminReceiverId,
                        principalTable: "AdminUserDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_AdminUserDatas_AdminSenderId",
                        column: x => x.AdminSenderId,
                        principalTable: "AdminUserDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_UserDatas_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "UserDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_UserDatas_SenderId",
                        column: x => x.SenderId,
                        principalTable: "UserDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryAdminUserDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_AdminUserDatas_SubCategoryAdminUserDataId",
                        column: x => x.SubCategoryAdminUserDataId,
                        principalTable: "AdminUserDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiscussionThreads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscussionThreadAdminUserDataId = table.Column<int>(type: "int", nullable: true),
                    DiscussionThreadUserDataId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionThreads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscussionThreads_AdminUserDatas_DiscussionThreadAdminUserDataId",
                        column: x => x.DiscussionThreadAdminUserDataId,
                        principalTable: "AdminUserDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DiscussionThreads_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionThreads_UserDatas_DiscussionThreadUserDataId",
                        column: x => x.DiscussionThreadUserDataId,
                        principalTable: "UserDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentUserDataId = table.Column<int>(type: "int", nullable: true),
                    CommentAdminUserDataId = table.Column<int>(type: "int", nullable: true),
                    CommentThreadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AdminUserDatas_CommentAdminUserDataId",
                        column: x => x.CommentAdminUserDataId,
                        principalTable: "AdminUserDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_DiscussionThreads_CommentThreadId",
                        column: x => x.CommentThreadId,
                        principalTable: "DiscussionThreads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_UserDatas_CommentUserDataId",
                        column: x => x.CommentUserDataId,
                        principalTable: "UserDatas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminLogs_LogsAdminUserDataId",
                table: "AdminLogs",
                column: "LogsAdminUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserDatas_UserId",
                table: "AdminUserDatas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatorAdminUserDataId",
                table: "Categories",
                column: "CreatorAdminUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentAdminUserDataId",
                table: "Comments",
                column: "CommentAdminUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentThreadId",
                table: "Comments",
                column: "ParentDiscussionThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentUserDataId",
                table: "Comments",
                column: "CommentUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionThreads_DiscussionThreadAdminUserDataId",
                table: "DiscussionThreads",
                column: "DiscussionThreadAdminUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionThreads_DiscussionThreadUserDataId",
                table: "DiscussionThreads",
                column: "DiscussionThreadUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionThreads_SubCategoryId",
                table: "DiscussionThreads",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AdminReceiverId",
                table: "Messages",
                column: "AdminReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AdminSenderId",
                table: "Messages",
                column: "AdminSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_ParentCategoryId",
                table: "SubCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_SubCategoryAdminUserDataId",
                table: "SubCategories",
                column: "CreatorAdminUserDataId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDatas_UserId",
                table: "UserDatas",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminLogs");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "DiscussionThreads");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "UserDatas");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AdminUserDatas");
        }
    }
}
