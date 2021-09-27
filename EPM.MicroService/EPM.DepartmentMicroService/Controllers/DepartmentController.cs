using EPM.DepartmentMicroService.Service;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.DeptResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPM.DepartmentMicroService.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseWithData<List<DeptResponseDto>>>> Get()
        {
            ApiResponseWithData<List<DeptResponseDto>> result = new ApiResponseWithData<List<DeptResponseDto>>().Success();
            var responseDto = await _service.GetListAsync();
            if (responseDto != null)
            {
                result.Data = responseDto.ToList();
                result.Count = responseDto.Count();
            }
            else
            {
                result = result.Fail();
            }
            return result;
        }

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] Department department)
        {
            ValidateResult validateResult = await _service.AddAsync(department);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put([FromBody] Department department)
        {
            ValidateResult validateResult = await _service.UpdateAsync(department);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }


        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            ValidateResult validateResult = await _service.DeleteAsync(id);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }
    }
}
