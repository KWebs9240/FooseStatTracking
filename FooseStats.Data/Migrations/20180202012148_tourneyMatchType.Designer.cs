using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FooseStats.Data.FooseStats.Data.Ef;

namespace FooseStats.Data.Migrations
{
    [DbContext(typeof(FooseStatsContext))]
    [Migration("20180202012148_tourneyMatchType")]
    partial class tourneyMatchType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("FooseStats.Data.FooseStats.Data.Ef.Entities.AlmaMater", b =>
                {
                    b.Property<Guid>("AlmaMaterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlmaMaterCode");

                    b.Property<string>("AlmaMaterDescription");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("AlmaMaterId");

                    b.ToTable("AlmaMaters");
                });

            modelBuilder.Entity("FooseStats.Data.FooseStats.Data.Ef.Entities.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("LocationCode");

                    b.Property<string>("LocationDescription");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("FooseStats.Data.FooseStats.Data.Ef.Entities.Match", b =>
                {
                    b.Property<Guid>("MatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

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

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("MatchTypeDescription");

                    b.Property<int>("MaxPoints");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("MatchTypeId");

                    b.ToTable("MatchTypes");
                });

            modelBuilder.Entity("FooseStats.Data.FooseStats.Data.Ef.Entities.Player", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlmaMaterId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("HexColor");

                    b.Property<string>("LastName");

                    b.Property<Guid>("LocationId");

                    b.Property<string>("NickName");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FooseStats.Data.FooseStats.Data.Ef.Entities.TournamentHeader", b =>
                {
                    b.Property<Guid>("TournamentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("HeadMatchId");

                    b.Property<Guid>("MatchTypeId");

                    b.Property<string>("TournamentName");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("TournamentId");

                    b.ToTable("TournamentHeaders");
                });

            modelBuilder.Entity("FooseStats.Data.FooseStats.Data.Ef.Entities.TournamentRelation", b =>
                {
                    b.Property<Guid>("TournamentRelationId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChildMatchId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("LeftParentMatchId");

                    b.Property<Guid>("RightParentMatchId");

                    b.Property<Guid>("TournamentHeaderId");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("TournamentRelationId");

                    b.ToTable("TournamentRelations");
                });
        }
    }
}
