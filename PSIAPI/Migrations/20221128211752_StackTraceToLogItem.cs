using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSIAPI.Migrations
{
    /// <inheritdoc />
    public partial class StackTraceToLogItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StackTrace",
                table: "LogItems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StackTrace",
                table: "LogItems");
        }
    }
}
