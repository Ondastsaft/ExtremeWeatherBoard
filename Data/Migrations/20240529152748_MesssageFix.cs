using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class MesssageFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AdminUserDatas_ReceiverId",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AdminReceiverId",
                table: "Messages",
                column: "AdminReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AdminUserDatas_AdminReceiverId",
                table: "Messages",
                column: "AdminReceiverId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AdminUserDatas_AdminReceiverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AdminReceiverId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AdminUserDatas_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "AdminUserDatas",
                principalColumn: "Id");
        }
    }
}
