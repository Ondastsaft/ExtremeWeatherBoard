using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class NormalisedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserDatas_DiscussionThreadAdminUserDataId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionThreads_UserDatas_UserDataId",
                table: "DiscussionThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_AdminUserDatas_DiscussionThreadAdminUserDataId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_DiscussionThreadAdminUserDataId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "DiscussionThreadAdminUserDataId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "UserDataId",
                table: "DiscussionThreads",
                newName: "DiscussionThreadUserDataId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionThreads_UserDataId",
                table: "DiscussionThreads",
                newName: "IX_DiscussionThreads_DiscussionThreadUserDataId");

            migrationBuilder.RenameColumn(
                name: "DiscussionThreadAdminUserDataId",
                table: "Comments",
                newName: "CommentUserDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_DiscussionThreadAdminUserDataId",
                table: "Comments",
                newName: "IX_Comments_CommentUserDataId");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "SubCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryAdminUserDataId",
                table: "SubCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DiscussionThreadAdminUserDataId",
                table: "DiscussionThreads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiscussionThreadAdminUserDataId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CreatorAdminUserDataId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_SubCategoryAdminUserDataId",
                table: "SubCategories",
                column: "SubCategoryAdminUserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserDatas_CommentUserDataId",
                table: "Comments",
                column: "CommentUserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionThreads_UserDatas_DiscussionThreadUserDataId",
                table: "DiscussionThreads",
                column: "DiscussionThreadUserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_AdminUserDatas_SubCategoryAdminUserDataId",
                table: "SubCategories",
                column: "SubCategoryAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserDatas_CommentUserDataId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionThreads_UserDatas_DiscussionThreadUserDataId",
                table: "DiscussionThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_AdminUserDatas_SubCategoryAdminUserDataId",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_SubCategoryAdminUserDataId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "SubCategoryAdminUserDataId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "CreatorAdminUserDataId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "DiscussionThreadUserDataId",
                table: "DiscussionThreads",
                newName: "UserDataId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionThreads_DiscussionThreadUserDataId",
                table: "DiscussionThreads",
                newName: "IX_DiscussionThreads_UserDataId");

            migrationBuilder.RenameColumn(
                name: "CommentUserDataId",
                table: "Comments",
                newName: "DiscussionThreadAdminUserDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CommentUserDataId",
                table: "Comments",
                newName: "IX_Comments_DiscussionThreadAdminUserDataId");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "SubCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DiscussionThreadAdminUserDataId",
                table: "SubCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiscussionThreadAdminUserDataId",
                table: "DiscussionThreads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DiscussionThreadAdminUserDataId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_DiscussionThreadAdminUserDataId",
                table: "SubCategories",
                column: "DiscussionThreadAdminUserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserDatas_DiscussionThreadAdminUserDataId",
                table: "Comments",
                column: "DiscussionThreadAdminUserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionThreads_UserDatas_UserDataId",
                table: "DiscussionThreads",
                column: "UserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_AdminUserDatas_DiscussionThreadAdminUserDataId",
                table: "SubCategories",
                column: "DiscussionThreadAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");
        }
    }
}
