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
        public DbSet<wmcb.model.User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Ground> Grounds { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsFeed>().ToTable("NewsFeed");
            modelBuilder.Entity<wmcb.model.User>().ToTable("Users");
            modelBuilder.Entity<Schedule>().ToTable("TempSchedule");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<Point>().ToTable("Points");
        }
    }
}
