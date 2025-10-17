using UMS.Domain.Entities;

namespace UMS.Infrastructure.Interface
{
    public interface IUserRepository
    {
        Task<int> RegisterAsync(User dto);
        Task<User> GetUserByIdAsync(Guid id);
        Task<AuthenticationResponse> FindByEmailAsync(string Email);
        Task<UserDetail> GetPasswordAsync(Guid id);
        Task<UserRole> GetRoleAsync(Guid id);
    }
}
