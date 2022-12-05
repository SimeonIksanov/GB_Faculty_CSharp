using System;
using Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.EF
{
    public class TimesheetDbContext : DbContext
    {
        public TimesheetDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Sheet> Sheets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        }
    }
}
