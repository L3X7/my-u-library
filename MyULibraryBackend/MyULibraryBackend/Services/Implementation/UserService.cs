using AutoMapper;
using Azure.Core;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;
using MyULibraryBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mp;
        private readonly IPasswordHasherService _passwordHasherService;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mp = mapper;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto request)
        {
            var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUsername != null)
                throw new Exception($"Username '{request.Username}' is already taken.");

            var existingEmail = await _userRepository.GetByEmailAsync(request.Email);
            if (existingEmail != null)
                throw new Exception($"Email '{request.Email}' is already registered.");

            string passwordHash = _passwordHasherService.Hash(request.PasswordHash);
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                CreatedDate = DateTime.UtcNow,
                Roles = new List<Role>()
            };

            if (request.RoleIds != null && request.RoleIds.Any())
            {
                var newRoles = await _roleRepository.GetByIdsAsync(request.RoleIds);
                if (newRoles.Count != request.RoleIds.Count)
                    throw new Exception("One or more invalid Role IDs provided.");
                foreach (var role in newRoles)
                {
                    user.Roles.Add(role);
                }
            }
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return new UserDto
            {
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Roles = user.Roles.Select(r => r.RoleName).ToList()
            };
        }

        public async Task DeleteUserAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mp.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mp.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByUsernameAsync(string name)
        {
            var user = await _userRepository.GetByUsernameAsync(name);
            return _mp.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(long id, UpdateUserDto request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Username = request.Username;
            user.PasswordHash = request.PasswordHash;
            user.Email = request.Email;
            if (request.RoleIds != null)
            {
                var newRoles = await _roleRepository.GetByIdsAsync(request.RoleIds);
                if (newRoles.Count != request.RoleIds.Count)
                    throw new Exception("One or more invalid Role IDs provided.");

                user.Roles.Clear();
                foreach (var role in newRoles)
                {
                    user.Roles.Add(role);
                }
            }
            await _userRepository.SaveChangesAsync();
        }
    }

}
