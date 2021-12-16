using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Migrations
{
    public partial class updatingMemberTableDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LibraryName",
                table: "Members",
                type: "nvarchar(max)",
                defaultValue:"No Data",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Members",
                type: "nvarchar(max)",
                defaultValue: "No Data",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "languagesKnown",
                table: "Members",
                type: "nvarchar(max)",
                defaultValue: "No Data",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LibraryName",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "languagesKnown",
                table: "Members");
        }
    }
}
