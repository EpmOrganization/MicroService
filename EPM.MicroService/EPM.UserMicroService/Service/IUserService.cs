using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.UserMicroService.Service
{
    public interface IUserService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ValidateResult> AddAsync(User entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ValidateResult> UpdateAsync(User entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ValidateResult> DeleteAsync(Guid id);

        Task<IEnumerable<User>> GetListAsync();

    }
}
