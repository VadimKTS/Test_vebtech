using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApplicationForVebtech.Migrations
{
    /// <inheritdoc />
    public partial class Initial_103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsertId",
                table: "RoleUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsertId",
                table: "RoleUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
