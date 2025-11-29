using AutoMapper;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;
using MyULibraryBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mp;

        public RoleService(IRoleRepository roleRepository, IMapper mp)
        {
            _roleRepository = roleRepository;
            _mp = mp;
        }

        public async Task<RoleDto> CreateRoleAsync(CreateRoleDto request)
        {
            var existingRole = await _roleRepository.GetByRoleNameAsync(request.RoleName);
            if (existingRole != null)
                throw new Exception($"Rolename '{request.RoleName}' is already taken.");
            var newRole = new Role
            {
                RoleName = request.RoleName
            };
            await _roleRepository.AddAsync(newRole);
            await _roleRepository.SaveChangesAsync();

            return new RoleDto
            {
                RoleName = request.RoleName,
            };

        }

        public async Task DeleteRoleAsync(long id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if(role == null)
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            _roleRepository.Delete(role);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mp.Map<List<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetRoleByIdAsync(long id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return _mp.Map<RoleDto>(role);
        }

        public async Task UpdateRoleAsync(long id, UpdateRoleDto request)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                throw new KeyNotFoundException($"Rol with ID {id} not found.");
            role.RoleName = request.RoleName;
            await _roleRepository.SaveChangesAsync();
        }
    }
}
