using UMS.Application.Wrappers;
using UMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<User>> Register(User dto);
        Task<ApiResponse<User>> GetUserById(Guid id);
        Task<ApiResponse<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
    }
}
