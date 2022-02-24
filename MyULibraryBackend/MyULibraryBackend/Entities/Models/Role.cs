using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyULibraryBackend.Entities.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRole { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string RoleName { get; set; }

        public List<User> Users { get; set; }
    }
}
