using EPM.Core.Helper;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Enum;
using EPM.RoleMicroService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.RoleMicroService.Service
{
    public class RoleService : BaseResult, IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }


        public async Task<ValidateResult> AddAsync(Role entity)
        {
            ValidateResult validateResult = new ValidateResult();
            // 判断是否已经存在要新增的用户
            Role role = await _repository.GetEntityAsync(p => p.Name == entity.Name && p.IsDeleted == (int)DeleteFlag.NotDeleted);
            if (role != null)
            {
                validateResult.Code = (int)CustomerCode.UserIsExist;
                validateResult.Msg = EnumHelper.GetEnumDesc(CustomerCode.UserIsExist);
            }
            else
            {
                entity.CreateUser = entity.UpdateUser = "admin";
                validateResult = await _repository.Add(entity) > 0 ? ReturnSuccess() : ReturnFail();
            }

            return validateResult;
        }

        public async Task<ValidateResult> DeleteAsync(Guid id)
        {
            var role = await _repository.GetEntityAsync(p => p.ID == id);
            // 
            role.IsDeleted = (int)DeleteFlag.Deleted;
            role.UpdateTime = DateTime.Now;
            role.UpdateUser = "admin";

            Expression<Func<Role, object>>[] updatedProperties =
            {
               p=>p.UpdateUser,
               p=>p.UpdateTime
            };
            // 保存数据
            return await _repository.Update(role, updatedProperties) > 0 ? ReturnSuccess() : ReturnFail();
        }

        public async Task<IEnumerable<Role>> GetListAsync()
        {
            return await _repository.GetAllListAsync(null);
        }

        public async Task<ValidateResult> UpdateAsync(Role entity)
        {
            // 从传递的token中获取用户信息
            var role = await _repository.GetEntityAsync(p => p.ID == entity.ID);
            role.Name = entity.Name;
            role.Description = entity.Description;
            role.UpdateTime = DateTime.Now;
            role.UpdateUser = "admin";
            // 用表达式树，更新部分字段
            Expression<Func<Role, object>>[] updatedProperties =
            {
               p=>p.Name,
               p=>p.Description,
               p=>p.UpdateUser,
               p=>p.UpdateTime
            };
            // 保存数据
            return await _repository.Update(role, updatedProperties) > 0 ? ReturnSuccess() : ReturnFail();
        }
    }
}
