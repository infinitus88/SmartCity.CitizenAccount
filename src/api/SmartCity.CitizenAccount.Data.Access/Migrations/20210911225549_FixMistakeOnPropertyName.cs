using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class FixMistakeOnPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Users_UserId",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Emails_UserId",
                table: "Emails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Emails_UserId",
                table: "Emails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Users_UserId",
                table: "Emails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
