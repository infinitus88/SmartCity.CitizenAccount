﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCity.CitizenAccount.Data.Access.Migrations
{
    public partial class AddRegistrationDatePropertyToServiceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Citizens",
                keyColumn: "Id",
                keyValue: "61425178-DADA-4CAF-87AB-46A0A7BDA371");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Services",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Citizens",
                columns: new[] { "Id", "Balance", "DateOfBirth", "FullName", "Gender", "IsDeleted", "PhotoUrl", "RegistrationDate" },
                values: new object[] { "1B427852-ECC8-4BAB-9503-1821041F491D", 0m, new DateTime(2001, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rustam Minnikhanov", (byte)0, false, "data:image/jpeg;base64,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", new DateTime(2021, 10, 4, 12, 13, 18, 223, DateTimeKind.Local).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$VozNZoTtM8oAxh6XCGuyIeJE/22OY72bkAFg/w8jVBSF8R0vqd4vG");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Citizens",
                keyColumn: "Id",
                keyValue: "1B427852-ECC8-4BAB-9503-1821041F491D");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Services");

            migrationBuilder.InsertData(
                table: "Citizens",
                columns: new[] { "Id", "Balance", "DateOfBirth", "FullName", "Gender", "IsDeleted", "PhotoUrl", "RegistrationDate" },
                values: new object[] { "61425178-DADA-4CAF-87AB-46A0A7BDA371", 0m, new DateTime(2001, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rustam Minnikhanov", (byte)0, false, "data:image/jpeg;base64,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", new DateTime(2021, 10, 3, 18, 40, 21, 670, DateTimeKind.Local).AddTicks(4228) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$PaCISozZgC9IVn6QS3xaZe7zGaGNxoTbuh0w29rSUO0tYZeIixbrO");
        }
    }
}
