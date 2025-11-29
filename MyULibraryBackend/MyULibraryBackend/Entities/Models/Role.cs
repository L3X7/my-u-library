using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyULibraryBackend.Entities.Models
{
    public class Role
    {
        public long Id { get; set; }

        [MaxLength(50)]
        public string RoleName { get; set; } = string.Empty;

        [JsonIgnore]
        public List<User> Users { get; set; } = null!;

    }
}
    