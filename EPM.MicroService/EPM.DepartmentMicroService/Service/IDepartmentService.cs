using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.DeptResponse;

namespace EPM.DepartmentMicroService.Service
{
    public interface IDepartmentService
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ValidateResult> AddAsync(Department entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ValidateResult> UpdateAsync(Department entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ValidateResult> DeleteAsync(Guid id);

        Task<IEnumerable<DeptResponseDto>> GetListAsync();
    }
}
