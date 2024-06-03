using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class CatFKUADFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AdminUserDatas_DiscussionThreadAdminUserDataId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DiscussionThreadAdminUserDataId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DiscussionThreadAdminUserDataId",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatorAdminUserDataId",
                table: "Categories",
                column: "CreatorAdminUserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AdminUserDatas_CreatorAdminUserDataId",
                table: "Categories",
                column: "CreatorAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AdminUserDatas_CreatorAdminUserDataId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatorAdminUserDataId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "DiscussionThreadAdminUserDataId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DiscussionThreadAdminUserDataId",
                table: "Categories",
                column: "DiscussionThreadAdminUserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AdminUserDatas_DiscussionThreadAdminUserDataId",
                table: "Categories",
                column: "DiscussionThreadAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");
        }
    }
}
