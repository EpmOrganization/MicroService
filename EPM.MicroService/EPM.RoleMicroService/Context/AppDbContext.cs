using EPM.Model.DbModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace EPM.RoleMicroService.Context
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
        public DbSet<Role> Roles { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 填充种子数据

            // 填充用户数据
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                ClusterID = 1,
                ID = Guid.NewGuid(),
                CreateUser = "系统管理员",
                UpdateUser = "系统管理员",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Name = "系统管理员",
            });
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
