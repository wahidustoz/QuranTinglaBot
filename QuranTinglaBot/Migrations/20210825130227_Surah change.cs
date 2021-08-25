using Microsoft.EntityFrameworkCore.Migrations;

namespace QuranTinglaBot.Migrations
{
    public partial class Surahchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Surahs",
                table: "Surahs");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Surahs",
                newName: "FileUniqueId");

            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "Surahs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surahs",
                table: "Surahs",
                column: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Surahs",
                table: "Surahs");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Surahs");

            migrationBuilder.RenameColumn(
                name: "FileUniqueId",
                table: "Surahs",
                newName: "MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surahs",
                table: "Surahs",
                column: "MessageId");
        }
    }
}
