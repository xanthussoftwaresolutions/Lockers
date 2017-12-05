using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public int LoginUserId { get; set; }
        public int Pin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public int? LockerId { get; set; }
    }

   
}