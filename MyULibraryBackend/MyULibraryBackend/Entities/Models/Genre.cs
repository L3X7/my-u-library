using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyULibraryBackend.Entities.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string GenreName { get; set; } = string.Empty;
    }
}
