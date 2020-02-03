using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RunningLogApp.Website.Migrations
{
    public partial class UpdateTotals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Athlete_RunTotals_AllTimeRunTotalsId",
                table: "Athlete");

            migrationBuilder.DropForeignKey(
                name: "FK_Athlete_RunTotals_RecentRunTotalsId",
                table: "Athlete");

            migrationBuilder.DropForeignKey(
                name: "FK_Athlete_RunTotals_YearToDateRunTotalsId",
                table: "Athlete");

            migrationBuilder.DropTable(
                name: "RunTotals");

            migrationBuilder.DropIndex(
                name: "IX_Athlete_AllTimeRunTotalsId",
                table: "Athlete");

            migrationBuilder.DropColumn(
                name: "AllTimeRunTotalsId",
                table: "Athlete");

            migrationBuilder.AlterColumn<int>(
                name: "YearToDateRunTotalsId",
                table: "Athlete",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecentRunTotalsId",
                table: "Athlete",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AllRunTotalsId",
                table: "Athlete",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TotalsData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    MovingTime = table.Column<TimeSpan>(nullable: false),
                    ElapsedTime = table.Column<TimeSpan>(nullable: false),
                    ElevationGain = table.Column<double>(nullable: false),
                    AchievementCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalsData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_AllRunTotalsId",
                table: "Athlete",
                column: "AllRunTotalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Athlete_TotalsData_AllRunTotalsId",
                table: "Athlete",
                column: "AllRunTotalsId",
                principalTable: "TotalsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Athlete_TotalsData_RecentRunTotalsId",
                table: "Athlete",
                column: "RecentRunTotalsId",
                principalTable: "TotalsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Athlete_TotalsData_YearToDateRunTotalsId",
                table: "Athlete",
                column: "YearToDateRunTotalsId",
                principalTable: "TotalsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Athlete_TotalsData_AllRunTotalsId",
                table: "Athlete");

            migrationBuilder.DropForeignKey(
                name: "FK_Athlete_TotalsData_RecentRunTotalsId",
                table: "Athlete");

            migrationBuilder.DropForeignKey(
                name: "FK_Athlete_TotalsData_YearToDateRunTotalsId",
                table: "Athlete");

            migrationBuilder.DropTable(
                name: "TotalsData");

            migrationBuilder.DropIndex(
                name: "IX_Athlete_AllRunTotalsId",
                table: "Athlete");

            migrationBuilder.DropColumn(
                name: "AllRunTotalsId",
                table: "Athlete");

            migrationBuilder.AlterColumn<long>(
                name: "YearToDateRunTotalsId",
                table: "Athlete",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "RecentRunTotalsId",
                table: "Athlete",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<long>(
                name: "AllTimeRunTotalsId",
                table: "Athlete",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RunTotals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AchievementCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    Distance = table.Column<double>(type: "REAL", nullable: false),
                    ElapsedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ElevationGain = table.Column<double>(type: "REAL", nullable: false),
                    MovingTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunTotals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Athlete_AllTimeRunTotalsId",
                table: "Athlete",
                column: "AllTimeRunTotalsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Athlete_RunTotals_AllTimeRunTotalsId",
                table: "Athlete",
                column: "AllTimeRunTotalsId",
                principalTable: "RunTotals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Athlete_RunTotals_RecentRunTotalsId",
                table: "Athlete",
                column: "RecentRunTotalsId",
                principalTable: "RunTotals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Athlete_RunTotals_YearToDateRunTotalsId",
                table: "Athlete",
                column: "YearToDateRunTotalsId",
                principalTable: "RunTotals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
