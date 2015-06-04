using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model;
using wmcb.model.Data;
namespace wmcb.repo
{
    public class wmcbContext : DbContext
    {
        public DbSet<NewsFeed> NewsFeeds { get; set; }
        public DbSet<WmcbUser> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Ground> Grounds { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DbSet<TeamStats> TeamStats { get; set; }
        public DbSet<Match> Match { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsFeed>().ToTable("NewsFeed");
            modelBuilder.Entity<WmcbUser>().ToTable("Users");
            modelBuilder.Entity<Schedule>().ToTable("Schedule").HasRequired(s => s.Match);
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles").HasRequired(ur => ur.Role);
            modelBuilder.Entity<PlayerStats>().ToTable("PlayerStats");
            modelBuilder.Entity<TeamStats>().ToTable("TeamStats");
            modelBuilder.Entity<Match>().ToTable("Matches");
        }
    }
}
