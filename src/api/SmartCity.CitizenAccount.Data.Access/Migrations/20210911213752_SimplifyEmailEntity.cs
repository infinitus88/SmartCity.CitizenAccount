using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class SimplifyEmailEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailFolder",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "SenderImg",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "ToImg",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "ToName",
                table: "Emails");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Folder",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Emails",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Users_UserId",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Emails_UserId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "Folder",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Emails");

            migrationBuilder.AddColumn<string>(
                name: "MailFolder",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderImg",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToImg",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToName",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
