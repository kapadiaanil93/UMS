using UMS.Application.Exceptions;
using UMS.Application.Helper;
using UMS.Application.Interfaces;
using UMS.Application.Wrappers;
using UMS.Domain.Entities;
using UMS.Infrastructure.Interface;
using Microsoft.Extensions.Configuration;

namespace UMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<ApiResponse<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
        {
            var user = await _repo.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ApiResponse<AuthenticationResponse>($"User not registered with this {request.Email}");
            }

            var _password = await _repo.GetPasswordAsync(user.UserId);
            if (_password == null)
            {
                return new ApiResponse<AuthenticationResponse>($"Invalid user password.");
            }

            var isValidPassword = _password.Password.Equals(HelperUtility.Password(request.Password));
            if (!isValidPassword)
                return new ApiResponse<AuthenticationResponse>($"Invalid password.");

            var authenticationUser = new AuthenticationResponse
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
            };

            var roleDetail = await _repo.GetRoleAsync(user.UserId);
            if(roleDetail !=null)
            {
                authenticationUser.Role = roleDetail.Role;
            }
            authenticationUser.Token = HelperUtility.GenerateJwtToken(authenticationUser, _config);
            return new ApiResponse<AuthenticationResponse>(authenticationUser, "User authenticated successfully.");
        }

        public async Task<ApiResponse<UserSaveEntity>> Register(UserSaveEntity dto)
        {
            var existing = await _repo.FindByEmailAsync(dto.Email);
            if (existing != null)
                throw new ApiException("User already exists");

            dto.Password = HelperUtility.Password(dto.Password);
            var _res = await _repo.RegisterAsync(dto);

            if (_res == null)
            {
                throw new ApiException("Error accur while saving data");
            }

            return new ApiResponse<UserSaveEntity>(dto, "User save successfully.");
        }

        public async Task<ApiResponse<int>> DeleteUser(Guid id)
        {
            var _res = await _repo.DeleteAsync(id);
            if (_res == 0)
            {
                return new ApiResponse<int>($"Invalid user detail.");
            }
            return new ApiResponse<int>(_res, "User deleted successfully.");
        }

        public async Task<ApiResponse<UserUpdateEntity>> UpdateUser(UserUpdateEntity dto)
        {
            var existing = await _repo.GetUserByIdAsync(dto.UserId);
            if (existing == null)
                throw new ApiException("User does not exists");

            var _user = await _repo.UpdateAsync(dto);
            if(_user == null)
            {
                throw new ApiException("Error accur while updating user data");
            }
            return new ApiResponse<UserUpdateEntity>(dto, "User data is updated successfully.");

        }
        
        public async Task<ApiResponse<User>> GetUserById(Guid id)
        {
            var _userData = await _repo.GetUserByIdAsync(id);
            if(_userData==null)
            {
                return new ApiResponse<User>($"Invalid user detail.");
            }
            User userdata = new User
            {
                UserId = _userData.UserId,
                Email = _userData.Email,
                FirstName = _userData.FirstName,
                MiddleName = _userData.MiddleName,
                LastName = _userData.LastName,
                Mobile = _userData.Mobile,
                Gender = _userData.Gender,
                DoB = _userData.DoB,
                CreatedBy = _userData.CreatedBy,
                isVerified = _userData.isVerified,
                Role = _userData.Role
            };

            UserRole _userRole = await _repo.GetRoleAsync(id);
            if (_userRole != null)
            {
                userdata.Role = _userRole.Role;
            }

            return new ApiResponse<User>(userdata, "User Data featch successfully.") ;
        }
        
        public async Task<ApiResponse<List<User>>> GetAllUser()
        {
            var userdata = await _repo.GetAllUserAsync();
            if (userdata == null) {
                return new ApiResponse<List<User>>($"Users are not available.");
            }
            return new ApiResponse<List<User>>(userdata, "User data featch successfully.");
        }
    }
}
