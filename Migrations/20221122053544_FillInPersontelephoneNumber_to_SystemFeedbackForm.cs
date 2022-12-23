using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire_and_FeedbackForm.Migrations
{
    public partial class FillInPersontelephoneNumber_to_SystemFeedbackForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FillInPersontelephoneNumber",
                table: "SystemFeedbackForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "deal_with_person_telephoneNumber",
                table: "Questionnaires",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FillInPersontelephoneNumber",
                table: "SystemFeedbackForms");

            migrationBuilder.DropColumn(
                name: "deal_with_person_telephoneNumber",
                table: "Questionnaires");
        }
    }
}
