using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire_and_FeedbackForm.Migrations
{
    public partial class principal_change_to_deal_with_persons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "principal",
                table: "OrderPrincipalDataModels",
                newName: "deal_with_persons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deal_with_persons",
                table: "OrderPrincipalDataModels",
                newName: "principal");
        }
    }
}
