using EPM.DepartmentMicroService.Context;
using EPM.Model.DbModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EPM.DepartmentMicroService.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;

        public DepartmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllListAsync(Expression<Func<Department, bool>> predicate)
        {
            return predicate != null ? await _dbContext.Departments.Where(predicate).ToListAsync() : await _dbContext.Departments.ToListAsync();
        }

        public async Task<Department> GetEntityAsync(Expression<Func<Department, bool>> predicate)
        {
            return predicate != null ? await _dbContext.Departments.FirstOrDefaultAsync(predicate) : await _dbContext.Departments.FirstOrDefaultAsync();
        }

        public async Task<int> Update(Department entity, Expression<Func<Department, object>>[] updatedProperties)
        {
            _dbContext.Set<Department>().Attach(entity);

            foreach (var property in updatedProperties)
            {
                _dbContext.Entry(entity).Property(property).IsModified = true;
            }
            return await _dbContext.SaveChangesAsync();
        }
    }
}
