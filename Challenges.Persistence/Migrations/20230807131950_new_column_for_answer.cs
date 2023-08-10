using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenges.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class new_column_for_answer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                schema: "challenge",
                table: "QuestionAnswers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                schema: "challenge",
                table: "QuestionAnswers");
        }
    }
}
