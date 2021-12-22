using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.UserMicroService.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        Task<int> Add(User entity);

        /// <summary>
        /// 更新、删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Update(User entity, Expression<Func<User, object>>[] updatedProperties);

        /// <summary>
        /// 获取单个实体（条件）
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<User> GetEntityAsync(Expression<Func<User, bool>> predicate);

        /// <summary>
        /// 根据条件获取集合数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllListAsync(Expression<Func<User, bool>> predicate);

        Task<UserResponseDto> GetPatgeListAsync(PagingRequest pagingRequest);
    }
}
