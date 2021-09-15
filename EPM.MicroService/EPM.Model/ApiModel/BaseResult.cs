using EPM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Model.ApiModel
{
    public class BaseResult
    {
        public static ValidateResult ReturnSuccess()
        {
            return new ValidateResult()
            {
                Code = (int)CustomerCode.Success,
                Msg = "操作成功"
            };
        }

        public static ValidateResult ReturnFail()
        {
            return new ValidateResult()
            {
                Code = (int)CustomerCode.Fail,
                Msg = "操作失败"
            };
        }

        public static ValidateResult RoleAllot()
        {
            return new ValidateResult()
            {
                Code = 0,
                Msg = "该角色已经分配给用户"
            };
        }

        public static ValidateResult MenuHasBeenAssigned()
        {
            return new ValidateResult()
            {
                Code = 0,
                Msg = "该菜单已被分配给某些角色，请取消分配后再进行删除"
            };
        }

        public static ValidateResult ExistRole()
        {
            return new ValidateResult()
            {
                Code = 0,
                Msg = "已存在同名的角色，修改角色失败"
            };
        }

        public static ValidateResult ExistDepartment()
        {
            return new ValidateResult()
            {
                Code = 0,
                Msg = "已存在同名的部门名称，修改失败"
            };
        }
    }
}
