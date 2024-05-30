using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtremeWeatherBoard.Data.Migrations
{
    /// <inheritdoc />
    public partial class TextAddedToDiscussionThread : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "DiscussionThreads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "DiscussionThreads");
        }
    }
}
