using EPM.Model.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.UserMicroService.Context
{
    /// <summary>
    /// 数据上下文类，继承自DbContext
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// 通过构造函数给父类传参
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region DbSet属性

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 填充种子数据

            // 填充用户数据
            modelBuilder.Entity<User>().HasData(new User()
            {
                ClusterID = 1,
                ID = Guid.NewGuid(),
                CreateUser = "系统管理员",
                UpdateUser = "系统管理员",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                LoginName = "admin",
                Password = "e10adc3949ba59abbe56e057f20f883e",
                Name = "系统管理员",
                DepartmentName = "技术中心",
                RoleName = "超级管理员"
            });
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
