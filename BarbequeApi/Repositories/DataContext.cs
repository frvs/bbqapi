using BarbequeApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BarbequeApi.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barbeque>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Barbeque>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Person>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Person>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Barbeque> Barbeques { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
