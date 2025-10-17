using Microsoft.Extensions.Configuration;
using UMS.Application.Exceptions;
using UMS.Application.Helper;
using UMS.Application.Interfaces;
using UMS.Application.Wrappers;
using UMS.Domain.Entities;
using UMS.Domains.Entities;
using UMS.Infrastructure.Interface;

namespace UMS.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IConfiguration _config;
        private readonly IRoleRepository _repo;

        public RoleService(IConfiguration config, IRoleRepository repo)
        {
            _repo = repo;
            _config = config;
        }
        public async Task<ApiResponse<RoleSaveEntity>> Create(RoleSaveEntity dto)
        {
            var existing = await _repo.FindByNameAsync(dto.RoleName);
            if (existing != null)
                throw new ApiException("Role Name is already exists.");

            var _res = await _repo.AddAsync(dto);

            if (_res == null)
            {
                throw new ApiException("Error accur while saving data");
            }

            return new ApiResponse<RoleSaveEntity>(dto, "Role is save successfully.");
        }

        public async Task<ApiResponse<int>> Delete(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
            {
                throw new Exception("Role is does not exists");
            }
            var _res = await _repo.DeleteAsync(id);
            if (_res == 0)
            {
                return new ApiResponse<int>($"Invalid role detail.");
            }
            return new ApiResponse<int>(_res, "Role is deleted successfully.");
        }

        public async Task<ApiResponse<List<Role>>> GetAll()
        {
            var roledata = await _repo.GetAllAsync();
            if (roledata == null)
            {
                return new ApiResponse<List<Role>>($"Roles are not available.");
            }
            return new ApiResponse<List<Role>>(roledata, "User data featch successfully.");
        }

        public async Task<ApiResponse<Role>> GetById(int id)
        {
            var _role = await _repo.GetByIdAsync(id);
            if (_role == null)
            {
                return new ApiResponse<Role>($"Role data is not available.");
            }

            return new ApiResponse<Role>(_role, "Role Data featch successfully.");
        }

        public async Task<ApiResponse<RoleUpdateEntity>> Update(RoleUpdateEntity dto)
        {
            var existing = await _repo.GetByIdAsync(dto.RoleId);
            if (existing == null)
                throw new ApiException("Role is does not exists");

            var _role = await _repo.UpdateAsync(dto);
            if (_role == null)
            {
                throw new ApiException("Error accur while updating role data");
            }
            return new ApiResponse<RoleUpdateEntity>(dto, "Role data is updated successfully.");
        }
    }
}
