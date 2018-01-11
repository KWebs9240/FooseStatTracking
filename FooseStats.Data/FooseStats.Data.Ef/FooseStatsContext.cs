using System;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;
using Microsoft.EntityFrameworkCore;

namespace FooseStats.Data.FooseStats.Data.Ef
{
    public class FooseStatsContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchType> MatchTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AlmaMater> AlmaMaters { get; set; }
        public DbSet<TournamentHeader> TournamentHeaders { get; set; }
        public DbSet<TournamentRelation> TournamentRelations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=FooseStats.db");
        }
    }
}
