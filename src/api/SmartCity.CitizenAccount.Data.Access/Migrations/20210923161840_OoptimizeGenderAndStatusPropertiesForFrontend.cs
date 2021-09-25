using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class OoptimizeGenderAndStatusPropertiesForFrontend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Citizens");

            migrationBuilder.AddColumn<byte>(
                name: "Gender",
                table: "Citizens",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Citizens");

            migrationBuilder.AddColumn<byte>(
                name: "Sex",
                table: "Citizens",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
