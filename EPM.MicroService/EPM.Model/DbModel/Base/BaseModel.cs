using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Model.DbModel.Base
{
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClusterID { get; set; }

        /// <summary>
        /// GUID类型
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();


        /// <summary>
        /// 创建用户
        /// </summary>
        public string? CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改用户
        /// </summary>
        public string? UpdateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Description { get; set; }
    }
}
