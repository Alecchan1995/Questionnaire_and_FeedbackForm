using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire_and_FeedbackForm.Migrations
{
    public partial class add_deal_with_person_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "deal_with_person",
                table: "Questionnaires",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deal_with_person",
                table: "Questionnaires");
        }
    }
}
