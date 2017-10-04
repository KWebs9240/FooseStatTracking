using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FooseStats.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(nullable: false),
                    IsDoubles = table.Column<bool>(nullable: false),
                    MatchTypeId = table.Column<Guid>(nullable: false),
                    Player1Id = table.Column<Guid>(nullable: false),
                    Player2Id = table.Column<Guid>(nullable: false),
                    Player3Id = table.Column<Guid>(nullable: false),
                    Player4Id = table.Column<Guid>(nullable: false),
                    Team1Score = table.Column<int>(nullable: false),
                    Team2Score = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                });

            migrationBuilder.CreateTable(
                name: "MatchTypes",
                columns: table => new
                {
                    MatchTypeId = table.Column<Guid>(nullable: false),
                    MatchTypeDescription = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTypes", x => x.MatchTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "MatchTypes");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
