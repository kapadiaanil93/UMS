using Dapper;
using UMS.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;
using UMS.Infrastructure.Interface;

namespace UMS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;
        private readonly IConfiguration _config;

        public UserRepository(IDbConnection db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<AuthenticationResponse> FindByEmailAsync(string Email)
        {
            return await _db.QueryFirstOrDefaultAsync<AuthenticationResponse>("SELECT Id As UserId, FirstName, Email FROM Users WHERE Email = @Email", new { Email });
        }

        public async Task<int> RegisterAsync(User dto)
        {
            var existing = await _db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { dto.Email });

            if (existing != null) throw new Exception("User already exists");

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = dto.Email,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Mobile = dto.Mobile,
                Gender = dto.Gender,
                DoB = dto.DoB,
                CreatedBy = "Admin",
                isVerified = true
            };

                if (_db.State != ConnectionState.Open)
                {
                    _db.Open();
                }

                using (var transaction = _db.BeginTransaction())
                {
                    try
                    {
                        // Insert into Users table
                        await _db.ExecuteAsync(
                            @"INSERT INTO Users (Id, Email, FirstName, MiddleName, LastName, Mobile, Gender, DoB, CreatedBy, isVerified)
                                VALUES (@UserId, @Email, @FirstName, @MiddleName, @LastName, @Mobile, @Gender, @DoB, @CreatedBy, @isVerified)",
                            user, transaction: transaction);

                        // Insert into UserDetail table
                        var userDetail = new UserDetail
                        {
                            UserId = user.UserId,
                            Password = dto.UserDetail.Password,
                            CreatedBy = "Admin"
                        };
                        await _db.ExecuteAsync(
                            @"INSERT INTO UserDetail (UserId, Password, CreatedBy)
                                VALUES (@UserId, @Password, @CreatedBy)",
                            userDetail, transaction: transaction);

                        // Insert into UserRole table
                        var userRole = new UserRole
                        {
                            UserId = user.UserId,
                            Role = dto.Role.Role,
                            CreatedBy = "Admin"
                        };
                        await _db.ExecuteAsync(
                            @"INSERT INTO UserRole (UserId, Role, CreatedBy)
                                VALUES (@UserId, @Role, @CreatedBy)",
                            userRole, transaction: transaction);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (_db.State == ConnectionState.Open)
                            _db.Close();
                    }
                }
                return 1;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
        }

        public async Task<UserDetail> GetPasswordAsync(Guid id)
        {
            return await _db.QueryFirstOrDefaultAsync<UserDetail>("SELECT UserId, Password FROM userdetail WHERE UserId = @Id", new { Id = id });
        }

        public async Task<UserRole> GetRoleAsync(Guid id)
        {
            return await _db.QueryFirstOrDefaultAsync<UserRole>("SELECT UserId, Role FROM userRole WHERE UserId = @Id", new { Id = id });
        }
    }
}
