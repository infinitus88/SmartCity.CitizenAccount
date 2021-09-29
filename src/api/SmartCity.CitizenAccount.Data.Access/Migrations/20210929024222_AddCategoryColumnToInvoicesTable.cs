using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class AddCategoryColumnToInvoicesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Category",
                table: "Invoices",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Invoices");
        }
    }
}
