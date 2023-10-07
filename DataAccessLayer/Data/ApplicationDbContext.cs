using System;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
	public class ApplicationDbContext : IdentityDbContext
    {
		public ApplicationDbContext()
		{
		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Admin> Admins { get; set; }

		public DbSet<Contractor> Contractors { get; set; }

		public DbSet<JobCode> JobCodes { get; set; }

        public DbSet<MachineCode> MachineCodes { get; set; }

        public DbSet<Timesheet> Timesheets { get; set; }

		public DbSet<LaborEntry> LaborEntries { get; set; }

		public DbSet<MachineEntry> MachineEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Server=localhost,1433;Database=DisasterRecoveryDB;TrustServerCertificate=True;MultiSubnetFailover=True";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

