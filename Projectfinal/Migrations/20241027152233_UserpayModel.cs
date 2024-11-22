using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectfinal.Migrations
{
    /// <inheritdoc />
    public partial class UserpayModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userpays");

            migrationBuilder.CreateTable(
                name: "UserPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Family = table.Column<string>(type: "TEXT", nullable: false),
                    Fullname = table.Column<string>(type: "TEXT", nullable: false),
                    NumberLone = table.Column<string>(type: "TEXT", nullable: false),
                    LoneCategory = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    OriginalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Interest = table.Column<decimal>(type: "TEXT", nullable: false),
                    RemainingBalance = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPayments");

            migrationBuilder.CreateTable(
                name: "userpays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Family = table.Column<string>(type: "TEXT", nullable: false),
                    Fullname = table.Column<string>(type: "TEXT", nullable: false),
                    Interest = table.Column<decimal>(type: "TEXT", nullable: false),
                    LoanCategory = table.Column<string>(type: "TEXT", nullable: false),
                    MoneyFirst = table.Column<decimal>(type: "TEXT", nullable: false),
                    MoneyLoanTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    NumberLoan = table.Column<string>(type: "TEXT", nullable: false),
                    UserPayAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userpays", x => x.Id);
                });
        }
    }
}
