using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectfinal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "Users");
        }
    }
}
