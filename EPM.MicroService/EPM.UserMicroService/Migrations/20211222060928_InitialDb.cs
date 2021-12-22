using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPM.UserMicroService.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ClusterID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID = table.Column<Guid>(nullable: false),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LoginName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    DepartmentName = table.Column<string>(nullable: true),
                    RoleName = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    LoginTime = table.Column<DateTime>(nullable: true),
                    PasswordUpdateTime = table.Column<DateTime>(nullable: true),
                    LoginErrorCount = table.Column<int>(nullable: false),
                    LoginLockTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ClusterID);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ClusterID", "CreateTime", "CreateUser", "DepartmentName", "Description", "EmailAddress", "ID", "IsDeleted", "LoginErrorCount", "LoginLockTime", "LoginName", "LoginTime", "MobileNumber", "Name", "Password", "PasswordUpdateTime", "Position", "RoleName", "Status", "UpdateTime", "UpdateUser" },
                values: new object[] { 1, new DateTime(2021, 12, 22, 14, 9, 28, 107, DateTimeKind.Local).AddTicks(7611), "系统管理员", "技术中心", null, null, new Guid("316e60b8-89c2-4d33-a31c-8740c54a4867"), 0, 0, null, "admin", null, null, "系统管理员", "e10adc3949ba59abbe56e057f20f883e", null, null, "超级管理员", 0, new DateTime(2021, 12, 22, 14, 9, 28, 107, DateTimeKind.Local).AddTicks(8326), "系统管理员" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
