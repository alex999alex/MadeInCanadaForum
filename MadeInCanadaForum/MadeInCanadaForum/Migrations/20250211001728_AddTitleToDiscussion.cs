using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadeInCanadaForum.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleToDiscussion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Discussion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Discussion");
        }
    }
}
