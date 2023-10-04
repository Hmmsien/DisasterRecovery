using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Entity
{
	public class JobCode
	{
		[Key]
		public int JobCodeID { get; set; }

        [Display(Name = "Job Code")]
		[Required]
		[MaxLength(30, ErrorMessage = "Cannot be more than 30 characters)")]
        public string JobCodeName { get; set; } = string.Empty;

        [Display(Name = "Job Description")]
		[Required]
		[MaxLength(200, ErrorMessage = "Cannot be more than 200 characters")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Hourly Rate")]
        [Required]
		[DataType(DataType.Currency)]
        public double HourlyRate { get; set; }

		[Display(Name = "Max Hours per Day")]
		[Required]
		[Range(typeof(int), "1", "8")]
		public int MaxHoursPerDay { get; set; }
	}
}
