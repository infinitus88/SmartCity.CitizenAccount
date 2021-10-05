using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class AddServicesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    SuccessRedirectUrl = table.Column<string>(nullable: true),
                    CancelRedirectUrl = table.Column<string>(nullable: true),
                    MarkPaidRequestUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ServiceId",
                table: "Invoices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Services_ServiceId",
                table: "Invoices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Services_ServiceId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ServiceId",
                table: "Invoices");
        }
    }
}
