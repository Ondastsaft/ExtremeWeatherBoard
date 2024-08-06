using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Migrations
{
    /// <inheritdoc />
    public partial class removednullableints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionThreads_UserDatas_DiscussionThreadUserDataId",
                table: "DiscussionThreads");

            migrationBuilder.AlterColumn<int>(
                name: "DiscussionThreadUserDataId",
                table: "DiscussionThreads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionThreads_UserDatas_DiscussionThreadUserDataId",
                table: "DiscussionThreads",
                column: "DiscussionThreadUserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionThreads_UserDatas_DiscussionThreadUserDataId",
                table: "DiscussionThreads");

            migrationBuilder.AlterColumn<int>(
                name: "DiscussionThreadUserDataId",
                table: "DiscussionThreads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionThreads_UserDatas_DiscussionThreadUserDataId",
                table: "DiscussionThreads",
                column: "DiscussionThreadUserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id");
        }
    }
}
