using System;

namespace MyULibraryBackend.Dtos
{
    public class UpdateBookLogDto
    {
        public long BookId { get; set; }
        public long UserId { get; set; }
        public DateTime LoanedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
