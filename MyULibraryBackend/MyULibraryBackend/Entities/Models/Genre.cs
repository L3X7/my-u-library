using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyULibraryBackend.Entities.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGenre { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string GenreName { get; set; }

    }
}
