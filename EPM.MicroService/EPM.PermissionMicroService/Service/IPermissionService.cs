using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPM.PermissionMicroService.Service
{
    public interface IPermissionService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ValidateResult> AddAsync(Menu entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ValidateResult> UpdateAsync(Menu entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ValidateResult> DeleteAsync(Guid id);

        Task<IEnumerable<Menu>> GetListAsync();
    }
}
