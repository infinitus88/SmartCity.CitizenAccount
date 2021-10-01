using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class ModifyInvoiceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Invoices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
