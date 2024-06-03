using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedrequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AdminUserDatas_CommentAdminUserDataId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "CommentAdminUserDataId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AdminUserDatas_CommentAdminUserDataId",
                table: "Comments",
                column: "CommentAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AdminUserDatas_CommentAdminUserDataId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "CommentAdminUserDataId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AdminUserDatas_CommentAdminUserDataId",
                table: "Comments",
                column: "CommentAdminUserDataId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
