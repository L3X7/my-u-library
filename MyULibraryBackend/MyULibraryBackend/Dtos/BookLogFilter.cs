using System;

namespace MyULibraryBackend.Dtos
{
    public class BookLogFilter
    {
        public string? BookTitle { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? LoanedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
