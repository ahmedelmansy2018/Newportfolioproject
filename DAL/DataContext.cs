using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BL.Enitites;

namespace DAL
{
   public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> Options):base(Options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    Id = Guid.NewGuid(),
                    FullName = "Ahmed ELMaNsY",
                    JobTittle = "Junior Full Stack .NET Developer",
                    ProfileImage="Ahmedelmansy.jpg"
                }) ;


            modelBuilder.Entity<Project>().Property(x => x.Id).HasDefaultValueSql("NEWID()");


            base.OnModelCreating(modelBuilder);         
        }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
       

    
}
    