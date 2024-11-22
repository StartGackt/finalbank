using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectfinal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyLoneTotal",
                table: "UserPayments");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MoneyTranss",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTranss_UserId",
                table: "MoneyTranss",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyTranss_Users_UserId",
                table: "MoneyTranss",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyTranss_Users_UserId",
                table: "MoneyTranss");

            migrationBuilder.DropIndex(
                name: "IX_MoneyTranss_UserId",
                table: "MoneyTranss");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MoneyTranss");

            migrationBuilder.AddColumn<decimal>(
                name: "MoneyLoneTotal",
                table: "UserPayments",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
