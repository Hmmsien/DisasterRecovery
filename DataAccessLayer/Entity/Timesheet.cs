using System;
namespace DataAccessLayer.Entity
{
	public class Timesheet
	{
		public int TimesheetID { get; set; }

		public string SiteCode { get; set; }

		public string ContractorName { get; set; }

		public DateTime SubmitedDate { get; set; }

		public IEnumerable<Labor> Labors { get; set; }

		public IEnumerable<Machine> Machines { get; set; }

		public string Status { get; set; }
	}
}