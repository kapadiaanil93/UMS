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

        public async Task<UserSaveEntity> RegisterAsync(UserSaveEntity dto)
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
                        Password = dto.Password,
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
                        Role = dto.Role,
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
            return dto;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var existing = await GetUserByIdAsync(id);

            if (existing == null) throw new Exception("User does not exists");

            var user = new User
            {
                UserId = id,
                Active = false,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "Admin"
            };

            if (_db.State != ConnectionState.Open)
            {
                _db.Open();
            }

            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    // Update into Users table
                    await _db.ExecuteAsync(@"Update Users Set Active = 0, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy Where Id = @UserId", user, transaction: transaction);

                    await _db.ExecuteAsync(
                        @"Update UserDetail Set Active = 0, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy Where userid = @UserId And Active = true",
                        user, transaction: transaction);

                    await _db.ExecuteAsync(
                        @"Update UserRole Set Active = 0, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy Where userid = @UserId And Active = true",
                        user, transaction: transaction);

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

        public async Task<UserUpdateEntity> UpdateAsync(UserUpdateEntity dto)
        {
            var user = new UserUpdateEntity
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Mobile = dto.Mobile,
                Gender = dto.Gender,
                DoB = dto.DoB,
                Role = dto.Role,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "Admin"
            };

            if (_db.State != ConnectionState.Open)
            {
                _db.Open();
            }

            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    await _db.ExecuteAsync(@"Update Users Set FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, 
                        Mobile = @Mobile, Gender = @Gender, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy Where Id = @UserId", user, transaction: transaction);

                    var _userrole = new UserRole
                    {
                        UserId = dto.UserId,
                        Role = dto.Role,
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "Admin"
                    };
                    await _db.ExecuteAsync(
                        @"Update UserRole Set Role = @Role, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy Where userid = @UserId And Active = true",
                        _userrole, transaction: transaction);

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
            return user;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {

            var _user = await _db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Active = true and Id = @Id", new { Id = id });
            if(_user != null)
            {
                _user.UserId = id;
            }
            return _user;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var _user = await _db.QueryAsync<User>("SELECT Id as UserId, Email, FirstName, MiddleName, LastName, Mobile, Gender, b.Role FROM Users a Inner Join (select Userid, Role from userrole where active = true) b  on b.userid = a.id WHERE Active = true");
            List<User> usersList = new List<User>();
            foreach (var user in _user)
            {
                usersList.Add(new User { UserId = user.UserId, Email = user.Email, FirstName = user.FirstName, MiddleName = user.MiddleName,
                    LastName = user.LastName, Gender = user.Gender, Mobile = user.Mobile, Role = user.Role
                });
            }
            return usersList;
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
