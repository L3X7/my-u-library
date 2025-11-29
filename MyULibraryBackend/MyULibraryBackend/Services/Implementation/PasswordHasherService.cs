namespace MyULibraryBackend.Services.Implementation
{
    public class PasswordHasherService: IPasswordHasherService
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string passwordHass)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHass);
        }
    }
}
