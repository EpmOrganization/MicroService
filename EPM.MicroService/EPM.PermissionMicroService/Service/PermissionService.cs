using EPM.Core.Helper;
using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Enum;
using EPM.PermissionMicroService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.PermissionMicroService.Service
{

    public class PermissionService : BaseResult, IPermissionService
    {
        private readonly IPermissionRepository _repository;

        public PermissionService(IPermissionRepository repository)
        {
            _repository = repository;
        }


        public async Task<ValidateResult> AddAsync(Menu entity)
        {
            ValidateResult validateResult = new ValidateResult();
            // 判断是否已经存在要新增的用户
            Menu Menu = await _repository.GetEntityAsync(p => p.Name == entity.Name && p.IsDeleted == (int)DeleteFlag.NotDeleted);
            if (Menu != null)
            {
                validateResult.Code = (int)CustomerCode.MenuIsExist;
                validateResult.Msg = EnumHelper.GetEnumDesc(CustomerCode.MenuIsExist);
            }
            else
            {
                entity.CreateUser = entity.CreateUser = "admin";
                validateResult = await _repository.Add(entity) > 0 ? ReturnSuccess() : ReturnFail();
            }

            return validateResult;
        }

        public async Task<ValidateResult> DeleteAsync(Guid id)
        {
            var Menu = await _repository.GetEntityAsync(p => p.ID == id);
            // 
            Menu.IsDeleted = (int)DeleteFlag.Deleted;
            Menu.UpdateTime = DateTime.Now;
            Menu.CreateUser = "admin";

            Expression<Func<Menu, object>>[] updatedProperties =
            {
               p=>p.IsDeleted,
               p=>p.CreateUser,
               p=>p.UpdateTime
            };
            // 保存数据
            return await _repository.Update(Menu, updatedProperties) > 0 ? ReturnSuccess() : ReturnFail();
        }

        public async Task<IEnumerable<Menu>> GetListAsync()
        {
            return await _repository.GetAllListAsync(null);
        }

        public async Task<ValidateResult> UpdateAsync(Menu entity)
        {
            // 从传递的token中获取用户信息
            var Menu = await _repository.GetEntityAsync(p => p.ID == entity.ID);
            Menu.Name = entity.Name;
            Menu.Description = entity.Description;
            Menu.UpdateTime = DateTime.Now;
            Menu.UpdateUser = "admin";
            // 用表达式树，更新部分字段
            Expression<Func<Menu, object>>[] updatedProperties =
            {
               p=>p.Name,
               p=>p.Description,
               p=>p.UpdateUser,
               p=>p.UpdateTime
            };
            // 保存数据
            return await _repository.Update(Menu, updatedProperties) > 0 ? ReturnSuccess() : ReturnFail();
        }
    }
}
