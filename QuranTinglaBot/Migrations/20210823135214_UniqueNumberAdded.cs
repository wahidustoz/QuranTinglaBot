using Microsoft.EntityFrameworkCore.Migrations;

namespace QuranTinglaBot.Migrations
{
    public partial class UniqueNumberAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Surahs_Number",
                table: "Surahs",
                column: "Number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Surahs_Number",
                table: "Surahs");
        }
    }
}
