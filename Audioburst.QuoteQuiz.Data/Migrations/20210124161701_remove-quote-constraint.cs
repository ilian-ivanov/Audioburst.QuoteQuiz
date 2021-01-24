using Microsoft.EntityFrameworkCore.Migrations;

namespace Audioburst.QuoteQuiz.Data.Migrations
{
    public partial class removequoteconstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quotes_Text",
                table: "Quotes");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Quotes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_Text",
                table: "Quotes",
                column: "Text",
                unique: true);
        }
    }
}
