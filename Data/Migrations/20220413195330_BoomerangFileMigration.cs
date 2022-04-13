using Microsoft.EntityFrameworkCore.Migrations;

namespace Boomerang.Data.Migrations
{
    public partial class BoomerangFileMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileByteArr",
                table: "Files",
                newName: "Content");

            migrationBuilder.AlterColumn<string>(
                name: "BelongsTo",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Files",
                newName: "FileByteArr");

            migrationBuilder.AlterColumn<int>(
                name: "BelongsTo",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
