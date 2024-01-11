using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elecookies.Entities {
    [Table("accounts")]
    public class Account {
        [Key]
        [Required]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Column("loginId", TypeName = "varchar(20)")]
        public string LoginId { get; set; }

        [Column("password", TypeName = "varchar(20)")]
        public string Password { get; set; }

        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column("address", TypeName = "varchar(100)")]
        public string Address { get; set; }
        [Column("phone", TypeName = "varchar(15)")]
        public string Phone { get; set; } = "";

        public Account(Guid id, string loginId, string password, string name, string email, string address) {
            Id = id;
            LoginId = loginId;
            Password = password;
            Name = name;
            Email = email;
            Address = address;
        }
    }
}
