using EPM.Model.DbModel;
using System.Linq.Expressions;

namespace EPM.DepartmentMicroService.Repositories
{
    public interface IDepartmentRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        Task<int> Add(Department entity);

        /// <summary>
        /// 更新、删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Update(Department entity, Expression<Func<Department, object>>[] updatedProperties);

        /// <summary>
        /// 获取单个实体（条件）
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<Department> GetEntityAsync(Expression<Func<Department, bool>> predicate);

        /// <summary>
        /// 根据条件获取集合数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<Department>> GetAllListAsync(Expression<Func<Department, bool>> predicate);
    }
}
