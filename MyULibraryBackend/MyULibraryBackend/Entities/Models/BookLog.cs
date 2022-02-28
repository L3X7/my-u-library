using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyULibraryBackend.Entities.Models
{
    public class BookLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdBookLog { get; set; }
        public long IdBook { get; set; }
        [ForeignKey("IdBook")]
        public Book Book { get; set; }
        public long IdUser { get; set; }
        [ForeignKey("IdUser")]
        public User User { get; set; }
        public DateTime LoanedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
