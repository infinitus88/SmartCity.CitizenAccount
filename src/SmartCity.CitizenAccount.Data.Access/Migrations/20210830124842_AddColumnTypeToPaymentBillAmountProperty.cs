using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class AddColumnTypeToPaymentBillAmountProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PaymentBills",
                type: "decimal(9,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PaymentBills",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");
        }
    }
}
