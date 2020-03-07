using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace Tienda.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string Phone { get; set; }
        public UserRole UserRole { get; set; }
        public string Password { get; set; }
    }

    public enum UserRole
    {
        Administrator,
        Client
    }
}
