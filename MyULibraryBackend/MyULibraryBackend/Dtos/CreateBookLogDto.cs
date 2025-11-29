using System;

namespace MyULibraryBackend.Dtos
{
    public class CreateBookLogDto
    {
        public long BookId { get; set; }
        public long UserId { get; set; }
        public DateTime LoanedDate { get; set; }
    }
}
