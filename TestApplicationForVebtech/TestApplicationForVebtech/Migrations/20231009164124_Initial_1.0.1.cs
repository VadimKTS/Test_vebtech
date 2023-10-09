using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApplicationForVebtech.Migrations
{
    /// <inheritdoc />
    public partial class Initial_101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("af93f255-c4fa-4692-9907-cbc517c38298"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "Name", "Password" },
                values: new object[] { new Guid("3db21ad9-a87f-45bb-b62c-037e130561be"), 30, "testUserEmail@mail.ru", "TestUser", "qwerty" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3db21ad9-a87f-45bb-b62c-037e130561be"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "Name", "Password" },
                values: new object[] { new Guid("af93f255-c4fa-4692-9907-cbc517c38298"), 30, "testUserEmail@mail.ru", "TestUser", "qwerty" });
        }
    }
}
