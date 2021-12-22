using EPM.Model.DbModel;
using EPM.RoleMicroService.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.RoleMicroService.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _dbContext;

        public RoleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(Role entity)
        {
            _dbContext.Roles.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public Task<int> Add(Consul.Role entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAllListAsync(Expression<Func<Role, bool>> predicate)
        {
            return predicate != null ? await _dbContext.Roles.Where(predicate).ToListAsync() : await _dbContext.Roles.ToListAsync();
        }

        public Task<IEnumerable<Consul.Role>> GetAllListAsync(Expression<Func<Consul.Role, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Role> GetEntityAsync(Expression<Func<Role, bool>> predicate)
        {
            return predicate != null ? await _dbContext.Roles.FirstOrDefaultAsync(predicate) : await _dbContext.Roles.FirstOrDefaultAsync();
        }

        public async Task<int> Update(Role entity, Expression<Func<Role, object>>[] updatedProperties)
        {
            _dbContext.Set<Role>().Attach(entity);

            foreach (var property in updatedProperties)
            {
                _dbContext.Entry(entity).Property(property).IsModified = true;
            }
            return await _dbContext.SaveChangesAsync();

        }

        public Task<int> Update(Consul.Role entity, Expression<Func<Consul.Role, object>>[] updatedProperties)
        {
            throw new NotImplementedException();
        }
    }
}
