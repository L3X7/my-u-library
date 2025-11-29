using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyULibraryBackend.Entities.Models
{
    public class RefreshToken
    {
        public long Id { get; set; }

        [MaxLength(1000)]
        public string JwtId { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Token { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpiredDate { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedDate { get; set; }

        public long UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
