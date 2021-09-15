using EPM.Core.Helper;
using EPM.Core.Security;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
using EPM.Model.Enum;
using EPM.UserMicroService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.UserMicroService.Service
{
    public class UserService :BaseResult, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task<ValidateResult> AddAsync(User entity)
        {
            ValidateResult validateResult = new ValidateResult();
            // 判断是否已经存在要新增的用户
            User user = await _repository.GetEntityAsync(p => p.LoginName == entity.LoginName && p.IsDeleted == (int)DeleteFlag.NotDeleted && p.Status == (int)UserStatus.Normal);
            if (user != null)
            {
                validateResult.Code = (int)CustomerCode.UserIsExist;
                validateResult.Msg = EnumHelper.GetEnumDesc(CustomerCode.UserIsExist);
            }
            else
            {
                entity.CreateUser = entity.UpdateUser = "admin";
                entity.Password = MD5Utility.Get32LowerMD5(entity.Password);
                entity.LoginErrorCount = 0;
                validateResult =await _repository.Add(entity) > 0 ? ReturnSuccess() : ReturnFail();
            }

            return validateResult;
        }

        public async Task<ValidateResult> DeleteAsync(Guid id)
        {
            var user = await _repository.GetEntityAsync(p => p.ID == id);
            // 
            user.IsDeleted = (int)DeleteFlag.Deleted;
            user.UpdateTime = DateTime.Now;
            user.UpdateUser = "admin";

            Expression<Func<User, object>>[] updatedProperties =
            {
               p=>p.Password,
               p=>p.UpdateUser,
               p=>p.UpdateTime
            };
            // 保存数据
            return await _repository.Update(user, updatedProperties) > 0 ? ReturnSuccess() : ReturnFail();
        }

        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await _repository.GetAllListAsync(null);
        }

        public async Task<ValidateResult> UpdateAsync(User entity)
        {
            // 从传递的token中获取用户信息
            var user = await _repository.GetEntityAsync(p => p.ID == entity.ID);
            user.Name = entity.Name;
            user.MobileNumber = entity.MobileNumber;
            user.Description = entity.Description;
            user.EmailAddress = entity.EmailAddress;
            user.DepartmentName = entity.DepartmentName;
            user.RoleName = entity.RoleName;
            user.UpdateTime = DateTime.Now;
            user.UpdateUser = "admin";
            // 用表达式树，更新部分字段
            Expression<Func<User, object>>[] updatedProperties =
            {
               p=>p.Name,
               p=>p.MobileNumber,
               p=>p.Description,
               p=>p.EmailAddress,
               p=>p.Position,
               p=>p.DepartmentName,
               p=>p.RoleName,
               p=>p.Status,
               p=>p.UpdateUser,
               p=>p.UpdateTime
            };
            // 保存数据
            return await _repository.Update(user, updatedProperties) > 0 ? ReturnSuccess() : ReturnFail();
        }
    }
}
