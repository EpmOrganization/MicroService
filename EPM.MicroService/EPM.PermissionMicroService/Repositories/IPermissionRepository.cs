using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.PermissionMicroService.Repositories
{
    public interface IPermissionRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        Task<int> Add(Menu entity);

        /// <summary>
        /// 更新、删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Update(Menu entity, Expression<Func<Menu, object>>[] updatedProperties);

        /// <summary>
        /// 获取单个实体（条件）
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<Menu> GetEntityAsync(Expression<Func<Menu, bool>> predicate);

        /// <summary>
        /// 根据条件获取集合数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<Menu>> GetAllListAsync(Expression<Func<Menu, bool>> predicate);
    }
}
