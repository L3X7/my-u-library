using MyULibraryBackend.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(long id);
        Task<RoleDto> CreateRoleAsync(CreateRoleDto request);
        Task UpdateRoleAsync(long id, UpdateRoleDto request);
        Task DeleteRoleAsync(long id);
    }
}
