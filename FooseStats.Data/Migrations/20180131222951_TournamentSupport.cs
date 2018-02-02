using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FooseStats.Data.Migrations
{
    public partial class TournamentSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentHeaders",
                columns: table => new
                {
                    TournamentId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    HeadMatchId = table.Column<Guid>(nullable: false),
                    TournamentName = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentHeaders", x => x.TournamentId);
                });

            migrationBuilder.CreateTable(
                name: "TournamentRelations",
                columns: table => new
                {
                    TournamentRelationId = table.Column<Guid>(nullable: false),
                    ChildMatchId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LeftParentMatchId = table.Column<Guid>(nullable: false),
                    RightParentMatchId = table.Column<Guid>(nullable: false),
                    TournamentHeaderId = table.Column<Guid>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentRelations", x => x.TournamentRelationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentHeaders");

            migrationBuilder.DropTable(
                name: "TournamentRelations");
        }
    }
}
