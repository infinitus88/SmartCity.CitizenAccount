using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class ModifyVerificationRequestEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CitizenId",
                table: "VerificationRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationRequests_CitizenId",
                table: "VerificationRequests",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationRequests_UserId",
                table: "VerificationRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationRequests_Citizens_CitizenId",
                table: "VerificationRequests",
                column: "CitizenId",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationRequests_Users_UserId",
                table: "VerificationRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerificationRequests_Citizens_CitizenId",
                table: "VerificationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationRequests_Users_UserId",
                table: "VerificationRequests");

            migrationBuilder.DropIndex(
                name: "IX_VerificationRequests_CitizenId",
                table: "VerificationRequests");

            migrationBuilder.DropIndex(
                name: "IX_VerificationRequests_UserId",
                table: "VerificationRequests");

            migrationBuilder.AlterColumn<string>(
                name: "CitizenId",
                table: "VerificationRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
