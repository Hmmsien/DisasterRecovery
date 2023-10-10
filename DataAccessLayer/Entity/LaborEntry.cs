using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Entity
{
	public class LaborEntry
	{
        [Key]
        public int LaborEntryID { get; set; }

        public int TimesheetID { get; set; }

        [Display(Name = "Job Code")]
        [Required]
        [MaxLength(30, ErrorMessage = "Cannot be more than 30 characters)")]
        public string JobCodeName { get; set; } = string.Empty;

        [Display(Name = "Hrs. worked")]
        [Required]
        public int HrsWorked { get; set; }

        [DataType(DataType.Currency)]
        public double TotalAmount { get; set; }
	}
}

