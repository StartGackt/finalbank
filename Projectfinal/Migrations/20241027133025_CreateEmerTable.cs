using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectfinal.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Family = table.Column<string>(type: "TEXT", nullable: false),
                    Fullname = table.Column<string>(type: "TEXT", nullable: false),
                    Username1 = table.Column<string>(type: "TEXT", nullable: false),
                    Fullname1 = table.Column<string>(type: "TEXT", nullable: false),
                    Phone1 = table.Column<string>(type: "TEXT", nullable: false),
                    Username2 = table.Column<string>(type: "TEXT", nullable: false),
                    Fullname2 = table.Column<string>(type: "TEXT", nullable: false),
                    Phone2 = table.Column<string>(type: "TEXT", nullable: false),
                    Username3 = table.Column<string>(type: "TEXT", nullable: false),
                    Fullname3 = table.Column<string>(type: "TEXT", nullable: false),
                    Phone3 = table.Column<string>(type: "TEXT", nullable: false),
                    MoneyOld = table.Column<double>(type: "REAL", nullable: false),
                    LoneMoney = table.Column<double>(type: "REAL", nullable: false),
                    TotalMoneyLone = table.Column<double>(type: "REAL", nullable: false),
                    NumberLone = table.Column<string>(type: "TEXT", nullable: false),
                    TimeLone = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Interrate = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emers");
        }
    }
}
