using EPM.Model.DbModel;
using EPM.PermissionMicroService.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.PermissionMicroService.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _dbContext;

        public PermissionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(Menu entity)
        {
            _dbContext.Menus.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllListAsync(Expression<Func<Menu, bool>> predicate)
        {
            return predicate != null ? await _dbContext.Menus.Where(predicate).ToListAsync() : await _dbContext.Menus.ToListAsync();
        }

        public async Task<Menu> GetEntityAsync(Expression<Func<Menu, bool>> predicate)
        {
            return predicate != null ? await _dbContext.Menus.FirstOrDefaultAsync(predicate) : await _dbContext.Menus.FirstOrDefaultAsync();
        }

        public async Task<int> Update(Menu entity, Expression<Func<Menu, object>>[] updatedProperties)
        {
            _dbContext.Set<Menu>().Attach(entity);

            foreach (var property in updatedProperties)
            {
                _dbContext.Entry(entity).Property(property).IsModified = true;
            }
            return await _dbContext.SaveChangesAsync();

        }
    }
}
