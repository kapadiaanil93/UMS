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

        public async Task<ApiResponse<User>> Register(User dto)
        {
            var existing = await _repo.FindByEmailAsync(dto.Email);
            if (existing != null) 
                throw new ApiException("User already exists");

            //dto.Role = "Admin";
            dto.UserDetail.Password = HelperUtility.Password(dto.UserDetail.Password);
            int? _res = await _repo.RegisterAsync(dto);

            if (_res == null || _res  == 0)
            {
                throw new ApiException("Error accur while saving data");
            }

            return new ApiResponse<User>(dto, "User save successfully.");
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
                userdata.Role = _userRole;
            }

            return new ApiResponse<User>(userdata, "User Data featch successfully.") ;
        }
        
    }
}
