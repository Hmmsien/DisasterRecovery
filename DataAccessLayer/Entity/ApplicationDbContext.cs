using System;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entity
{
	public class ApplicationDbContext : DbContext
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

		public DbSet<Timesheet> Timesheets { get; set; }

		public DbSet<Labor> Labors { get; set; }

		public DbSet<Machine> Machines { get; set; }
	}
}
