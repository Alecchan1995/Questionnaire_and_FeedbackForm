using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire_and_FeedbackForm.Migrations
{
    public partial class principal_column_to_Questionnaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "principal",
                table: "Questionnaires",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "principal",
                table: "Questionnaires");
        }
    }
}
