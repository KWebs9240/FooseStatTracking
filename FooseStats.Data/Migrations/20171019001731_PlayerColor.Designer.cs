using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FooseStats.Data.FooseStats.Data.Ef;

namespace FooseStats.Data.Migrations
{
    [DbContext(typeof(FooseStatsContext))]
    [Migration("20171019001731_PlayerColor")]
    partial class PlayerColor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("FooseStats.Data.FooseStats.Data.Ef.Entities.Match", b =>
                {
                    b.Property<Guid>("MatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDoubles");

                    b.Property<Guid>("MatchTypeId");

                    b.Property<Guid>("Player1Id");

                    b.Property<Guid>("Player2Id");

                    b.Property<Guid>("Player3Id");

                    b.Property<Guid>("Player4Id");

                    b.Property<int>("Team1Score");

                    b.Property<int>("Team2Score");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("MatchId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("FooseStats.Data.FooseStats.Data.Ef.Entities.MatchType", b =>
                {
                    b.Property<Guid>("MatchTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MatchTypeDescription");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("MatchTypeId");

                    b.ToTable("MatchTypes");
                });

            modelBuilder.Entity("FooseStats.Data.FooseStats.Data.Ef.Entities.Player", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("HexColor");

                    b.Property<string>("LastName");

                    b.Property<string>("NickName");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });
        }
    }
}
