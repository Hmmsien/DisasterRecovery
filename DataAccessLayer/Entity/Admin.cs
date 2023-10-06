using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Entity
{
    public class Admin
    {
        [Key]
        public string AdminID { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
