using UMS.Application.Wrappers;
using UMS.Domain.Entities;

namespace UMS.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UserSaveEntity>> Register(UserSaveEntity dto);
        Task<ApiResponse<int>> DeleteUser(Guid id);
        Task<ApiResponse<UserUpdateEntity>> UpdateUser(UserUpdateEntity dto);
        Task<ApiResponse<User>> GetUserById(Guid id);
        Task<ApiResponse<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
        Task<ApiResponse<List<User>>> GetAllUser();
    }
}
