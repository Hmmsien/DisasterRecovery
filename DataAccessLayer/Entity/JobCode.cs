using System;
namespace DataAccessLayer.Entity
{
	public class JobCode
	{
		public int JobCodeID { get; set; }

		public string JobCodeName { get; set; }

		public string Description { get; set; }

		public double HourlyRate { get; set; }

		public double MaxHourPerDay { get; set; }
	}
}
