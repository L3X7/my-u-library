using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Dtos
{
    public class UserDto
    {
        public long IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int IdRole { get; set; }
        public string Password { get; set; }
    }
}
