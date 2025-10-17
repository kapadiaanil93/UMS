using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Entities;
using UMS.Domains.Entities;

namespace UMS.Infrastructure.Interface
{
    public interface IRoleRepository
    {
        Task<RoleSaveEntity> AddAsync(RoleSaveEntity dto);
        Task<int> DeleteAsync(int id);
        Task<RoleUpdateEntity> UpdateAsync(RoleUpdateEntity dto);
        Task<Role> FindByNameAsync(string RoleName);
        Task<Role> GetAsync(Role id);
        Task<List<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int id);
    }
}
