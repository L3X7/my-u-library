using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Dtos
{
    public class BookDto
    {
        public long IdBook { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public int IdGenre { get; set; }
        public int Quantity { get; set; }
    }
}
