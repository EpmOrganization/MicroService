using EPM.Model.DbModel;
using Microsoft.EntityFrameworkCore;

namespace EPM.DepartmentMicroService.Context
{
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
        public DbSet<Department> Departments { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 填充种子数据

            // 填充用户数据
            //modelBuilder.Entity<Department>().HasData(new Department()
            //{
            //    ClusterID = 1,
            //    ID = Guid.NewGuid(),
            //    CreateUser = "系统管理员",
            //    UpdateUser = "系统管理员",
            //    CreateTime = DateTime.Now,
            //    UpdateTime = DateTime.Now,
            //    Name = "技术中心",
            //    ParentID=Guid.Empty
            //});
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
