using System;

namespace MyULibraryBackend.Dtos
{
    public class BookLogDto
    {
        public long IdBookLog { get; set; }
        public long IdBook { get; set; }
        public string BookTitle { get; set; }
        public long IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime LoanedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
