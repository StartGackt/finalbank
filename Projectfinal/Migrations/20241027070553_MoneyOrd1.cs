using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectfinal.Migrations
{
    /// <inheritdoc />
    public partial class MoneyOrd1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdLones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Family = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
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
                    MoneyOld = table.Column<int>(type: "INTEGER", nullable: false),
                    LoneMoney = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberLone = table.Column<string>(type: "TEXT", nullable: false),
                    TimeLone = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LoneMoney1 = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalMoneyLone = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdLones", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdLones");
        }
    }
}
