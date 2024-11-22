using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectfinal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Idcard",
                table: "Users",
                newName: "IdCard");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Users",
                newName: "User2");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Users",
                newName: "User1");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PhoneUser2");

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneUser1",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Family",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneUser1",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "IdCard",
                table: "Users",
                newName: "Idcard");

            migrationBuilder.RenameColumn(
                name: "User2",
                table: "Users",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "User1",
                table: "Users",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "PhoneUser2",
                table: "Users",
                newName: "Password");
        }
    }
}
