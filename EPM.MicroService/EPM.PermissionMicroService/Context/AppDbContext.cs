using EPM.Model.DbModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace EPM.PermissionMicroService.Context
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
        /// 菜单表
        /// </summary>
        public DbSet<Menu> Menus { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 填充种子数据

            var parentId = Guid.NewGuid();
            // 填充菜单数据
            modelBuilder.Entity<Menu>().HasData(new Menu()
            {
                ClusterID = 1,
                ID = parentId,
                CreateUser = "系统管理员",
                UpdateUser = "系统管理员",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Name = "系统管理",
            },
            new Menu()
            {
                ClusterID = 2,
                ID = Guid.NewGuid(),
                CreateUser = "系统管理员",
                UpdateUser = "系统管理员",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                ParentMenuID=parentId,
                ParentList=parentId.ToString(),
                Name = "用户管理",
            },
            new Menu()
            {
                ClusterID = 3,
                ID = Guid.NewGuid(),
                CreateUser = "系统管理员",
                UpdateUser = "系统管理员",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                ParentMenuID = parentId,
                ParentList = parentId.ToString(),
                Name = "角色管理",
            },
            new Menu()
            {
                ClusterID = 4,
                ID = Guid.NewGuid(),
                CreateUser = "系统管理员",
                UpdateUser = "系统管理员",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                ParentMenuID = parentId,
                ParentList = parentId.ToString(),
                Name = "部门管理",
            });
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
