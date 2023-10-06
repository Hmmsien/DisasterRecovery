using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
	public class MachineCode
	{
        [Key]
        public int MachineCodeID { get; set; }

        [Display(Name = "Machine Code")]
        [Required]
        [MaxLength(30, ErrorMessage = "Cannot be more than 30 characters)")]
        public string MachineCodeName { get; set; } = string.Empty;

        [Display(Name = "Machine Description")]
        [Required]
        [MaxLength(200, ErrorMessage = "Cannot be more than 200 characters")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Usage Rate")]
        [Required]
        [DataType(DataType.Currency)]
        public double UsageRate { get; set; }

        [Display(Name = "Max Hours Per Day")]
        [Required]
        [Range(typeof(int), "1", "8")]
        public int MaxHoursPerDay { get; set; }
    }
}
