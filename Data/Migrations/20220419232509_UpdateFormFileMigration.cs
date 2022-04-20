using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boomerang.Data.Migrations
{
    public partial class UpdateFormFileMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Files",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Files");
        }
    }
}
