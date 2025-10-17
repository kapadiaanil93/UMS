using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Entities;
using UMS.Domains.Entities;
using UMS.Infrastructure.Interface;

namespace UMS.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbConnection _db;

        public RoleRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<RoleSaveEntity> AddAsync(RoleSaveEntity dto)
        {
            var role = new Role
            {
                RoleId = 0,
                RoleName = dto.RoleName,
                Description = dto.Description,
                CreatedBy = "Admn"
            };

            if (_db.State != ConnectionState.Open)
            {
                _db.Open();
            }

            using (var transaction = _db.BeginTransaction())
            {
                try
                {
                    // Insert into Role_Master table
                    await _db.ExecuteAsync(
                        @"INSERT INTO Role_Master (RoleName, Description, CreatedBy)
                                VALUES (@RoleName, @Description, @CreatedBy)",
                        role, transaction: transaction);

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

        public async Task<int> DeleteAsync(int id)
        {
            var role = new Role
            {
                RoleId = id,
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
                    // Update into Role table
                    await _db.ExecuteAsync(@"Update Role_Master Set Active = 0, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy Where RoleId = @RoleId And Active = true", role, transaction: transaction);

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

        public async Task<Role> FindByNameAsync(string RoleName)
        {
            return await _db.QueryFirstOrDefaultAsync<Role>("SELECT * FROM Role_Master WHERE  Active = true And RoleName = @RoleName", new { @RoleName = RoleName });
        }

        public async Task<List<Role>> GetAllAsync()
        {
            var _role = await _db.QueryAsync<Role>("SELECT * FROM Role_Master Where Active = true");
            List<Role> roleList = new List<Role>();
            foreach (var role in _role)
            {
                roleList.Add(new Role
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                });
            }
            return roleList;
        }

        public async Task<Role> GetAsync(Role id)
        {
            return await _db.QueryFirstOrDefaultAsync<Role>("SELECT * FROM Role_Master Where Active = true");
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<Role>("SELECT * FROM Role_Master Where Active = true And RoleId = @RoleId", new { RoleId = id});
        }
        
        public async Task<RoleUpdateEntity> UpdateAsync(RoleUpdateEntity dto)
        {
            var role = new RoleUpdateEntity
            {
                RoleId = dto.RoleId,
                RoleName= dto.RoleName,
                Description = dto.Description,
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
                    await _db.ExecuteAsync(@"Update Role_Master Set RoleName = @RoleName, Description = @Description, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy Where RoleId = @RoleId", role, transaction: transaction);

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
            return role;
        }
    }
}
