using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyULibraryBackend.Entities.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdBook { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public int IdGenre { get; set; }
        [ForeignKey("IdGenre")]
        public Genre Genre { get; set; }
        public int Quantity { get; set; }

    }
}
