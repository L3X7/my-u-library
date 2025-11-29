using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyULibraryBackend.Entities.Models
{
    public class Book
    {
        public long Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Author { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
