using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyULibraryBackend.Entities.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUser { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Email { get; set; }
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public Role Role { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Password { get; set; }

    }    
}
