using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Entity
{
	public class MachineEntry
	{
        [Key]
        public int MachineEntryID { get; set; }

        public int TimesheetID { get; set; }

        [Display(Name = "Machine Code")]
        [Required]
        [MaxLength(30, ErrorMessage = "Cannot be more than 30 characters)")]
        public string MachineCodeName { get; set; } = string.Empty;

        [Display(Name = "Hrs. Used")]
        [Required]
        [DataType(DataType.Currency)]
        public int HrsUsed { get; set; }

        [DataType(DataType.Currency)]
        public double TotalAmount { get; set; }
    }
}
