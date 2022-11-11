using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questionnaire_and_FeedbackForm.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModelOptions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionColumn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionItem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelOptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SystemFeedbackForms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fill_In_Person = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    System_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Send_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deal_with_state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Problem_Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemFeedbackForms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Service_Fraction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Process_Fraction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Other_Idea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deal_with_idea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deal_with_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Questionnaires_SystemFeedbackForms_ID",
                        column: x => x.ID,
                        principalTable: "SystemFeedbackForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelOptions");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "SystemFeedbackForms");
        }
    }
}
