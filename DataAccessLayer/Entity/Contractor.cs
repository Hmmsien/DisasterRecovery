using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Entity
{
	public class Contractor
	{
        [Key]
		public int ContractorID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int TotalHrs = 0;

        [DataType(DataType.Currency)]
        public double TotalAmount = 0;

        public string UserId { get; set; } = string.Empty;
    }
}
