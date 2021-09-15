using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
using EPM.UserMicroService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.UserMicroService.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<ApiResponseWithData<List<User>>>> Get()
        {
            ApiResponseWithData<List<User>> result = new ApiResponseWithData<List<User>>().Success();

            var responseDto = await _userService.GetListAsync();
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
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody] User user)
        {
            ValidateResult validateResult = await _userService.AddAsync(user);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put([FromBody] User user)
        {
            ValidateResult validateResult = await _userService.UpdateAsync(user);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            ValidateResult validateResult = await _userService.DeleteAsync(id);
            return validateResult.Code > 0 ? ApiResponse.Success() : ApiResponse.Fail();
        }

    }
}
