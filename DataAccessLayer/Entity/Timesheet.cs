using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
	public class Timesheet
	{
		[Key]
		public int TimesheetID { get; set; }

		public string ContractorID { get; set; } = string.Empty;

        [Display(Name = "Site Code")]
        [Required]
        [MaxLength(10, ErrorMessage = "Cannot be more than 10 characters)")]
        public string SiteCode { get; set; } = string.Empty;

        public DateTime SubmitedDate { get; set; }

		public IEnumerable<LaborEntry> LaborEntries { get; set; } = default!;

		public IEnumerable<MachineEntry> MachineEntries { get; set; } = default!;

        public string Status { get; set; } = "Pending";

		public bool Approved { get; set; } = false;
	}
}