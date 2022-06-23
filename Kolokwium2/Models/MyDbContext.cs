using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Models
{

    public interface IMainMyDbContext
    {
        public DbSet<File> Files { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        //Task<int> SaveChangesAsync(CancellactionToken cancellactionToken = default);
    }

    public class MyDbContext : DbContext
    {
        protected MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<File>(f =>
            {
                f.HasKey(e => new { e.FileID, e.TeamID });
                f.Property(e => e.FileName).IsRequired();
                f.Property(e => e.FileExtension).IsRequired();
                f.Property(e => e.FileSize).IsRequired();
                f.HasOne(e => e.Team).WithMany(e => e.Files).HasForeignKey(e => e.TeamID);

               
            });

            modelBuilder.Entity<Team>(t => 
            {
                t.HasKey(e => e.TeamID);
                t.HasOne(e => e.Organization).WithMany(e => e.Teams).HasForeignKey(e => e.OrganizationID);
                t.Property(e => e.TeamName).IsRequired();
                t.Property(e => e.TeamDescription);
            
            });

            modelBuilder.Entity<Organization>(o =>
            {
                o.HasKey(e => e.OrganizationID);
                o.Property(e => e.OrganizationName).IsRequired();
                o.Property(e => e.OrganizationDomain).IsRequired();
            });

            modelBuilder.Entity<Member>(m =>
            {
                m.HasKey(e => e.MemberID);
                m.HasOne(e => e.Organization).WithMany(e => e.Members).HasForeignKey(e => e.OrganizationID);
                m.Property(e => e.MemberName).IsRequired();
                m.Property(e => e.MemberSurname).IsRequired();
                m.Property(e => e.MemberNickName);
            });

            modelBuilder.Entity<Membership>(m =>
            {
                m.HasKey(e => new { e.MemberID, e.TeamID });
                m.Property(e => e.MembershipDate).IsRequired();
                m.HasOne(e => e.Team).WithMany(e => e.Memberships).HasForeignKey(e => e.TeamID);
                m.HasOne(e => e.Member).WithMany(e => e.Memberships).HasForeignKey(e => e.MemberID);
            });
        }
    }
}
