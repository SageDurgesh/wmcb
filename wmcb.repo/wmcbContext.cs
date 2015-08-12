using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model;
using wmcb.model.Data;
using System.Data.Entity.Core.Metadata.Edm;
namespace wmcb.repo
{
    public class wmcbContext : DbContext
    {
        public DbSet<NewsFeed> NewsFeeds { get; set; }
        public DbSet<WmcbUser> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Ground> Grounds { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DbSet<TeamStats> TeamStats { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournament { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<MessageTemplateDto> MessageTemplate { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsFeed>().ToTable("NewsFeed");
            modelBuilder.Entity<WmcbUser>().ToTable("Users");
               // .HasMany(u => u.Teams).WithMany().Map(m => { m.MapLeftKey("ID"); m.MapRightKey("UserID"); m.ToTable("UserTeams"); });
            modelBuilder.Entity<UserTeam>().ToTable("UserTeams").HasRequired(u=>u.Team);
           // modelBuilder.Entity<Schedule>().ToTable("TempSchedule");//.HasRequired(s => s.Match);
            modelBuilder.Entity<Schedule>().ToTable("Schedule");//.HasRequired(s => s.Match);
            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles").HasRequired(ur => ur.Role);
            modelBuilder.Entity<Point>().ToTable("Points");
            modelBuilder.Entity<TeamStats>().ToTable("TeamStats");
            modelBuilder.Entity<Match>().ToTable("Matches").HasKey(m => m.ID) ;
            modelBuilder.Entity<Tournament>().ToTable("Tournament");
            modelBuilder.Entity<Division>().ToTable("Division");
            modelBuilder.Entity<Ground>().ToTable("Grounds");
            modelBuilder.Entity<MessageTemplateDto>().ToTable("MessageTemplate");
        }
    }
}
