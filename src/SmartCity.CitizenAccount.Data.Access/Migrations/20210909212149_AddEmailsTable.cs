using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class AddEmailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sender = table.Column<string>(nullable: true),
                    SenderName = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    ToName = table.Column<string>(nullable: true),
                    SenderImg = table.Column<string>(nullable: true),
                    ToImg = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    IsStarred = table.Column<bool>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    MailFolder = table.Column<string>(nullable: true),
                    Unread = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");
        }
    }
}
