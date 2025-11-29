using System;
using System.Text.Json.Serialization;

namespace MyULibraryBackend.Entities.Models
{
    public class BookLog
    {
        public long Id { get; set; }

        public long BookId { get; set; }

        [JsonIgnore]
        public Book Book { get; set; } = null!;

        public long UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;

        public DateTime LoanedDate { get; set; } = DateTime.Now;
        public DateTime? ReturnedDate { get; set; }
    }
}
