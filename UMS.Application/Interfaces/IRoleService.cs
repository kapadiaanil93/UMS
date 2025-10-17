using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Application.Wrappers;
using UMS.Domain.Entities;
using UMS.Domains.Entities;

namespace UMS.Application.Interfaces
{
    public interface IRoleService
    {
        Task<ApiResponse<RoleSaveEntity>> Create(RoleSaveEntity dto);
        Task<ApiResponse<int>> Delete(int id);
        Task<ApiResponse<RoleUpdateEntity>> Update(RoleUpdateEntity dto);
        Task<ApiResponse<Role>> GetById(int id);
        Task<ApiResponse<List<Role>>> GetAll();
    }
}
