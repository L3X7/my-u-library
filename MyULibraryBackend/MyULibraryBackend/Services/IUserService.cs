using MyULibraryBackend.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(long id);
        Task<UserDto> GetUserByUsernameAsync(string name);
        Task<UserDto> CreateUserAsync(CreateUserDto request);
        Task UpdateUserAsync(long id, UpdateUserDto request);
        Task DeleteUserAsync(long id);
    }
}
