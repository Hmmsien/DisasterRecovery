using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Entity
{
	public class Contractor
	{
        [Key]
		public string ContractorID { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
