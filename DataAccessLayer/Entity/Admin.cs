using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Entity
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
    }
}
