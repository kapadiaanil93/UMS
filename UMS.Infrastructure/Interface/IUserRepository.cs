using UMS.Domain.Entities;

namespace UMS.Infrastructure.Interface
{
    public interface IUserRepository
    {
        Task<UserSaveEntity> RegisterAsync(UserSaveEntity dto);
        Task<int> DeleteAsync(Guid id);
        Task<UserUpdateEntity> UpdateAsync(UserUpdateEntity dto);
        Task<User> GetUserByIdAsync(Guid id);
        Task<AuthenticationResponse> FindByEmailAsync(string Email);
        Task<UserDetail> GetPasswordAsync(Guid id);
        Task<UserRole> GetRoleAsync(Guid id);
        Task<List<User>> GetAllUserAsync();
    }
}
