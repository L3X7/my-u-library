using System.Collections.Generic;

namespace MyULibraryBackend.Dtos
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public List<long>? RoleIds { get; set; }

    }
}
