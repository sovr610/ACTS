using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StargateAPI.Business.Data
{
    [Index(nameof(UserName), IsUnique = true)]
    [Table("Account")]
    public class Account
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }

        public required string PasswordHash { get; set; }

        public required DateTime AccountCreated { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }
}
