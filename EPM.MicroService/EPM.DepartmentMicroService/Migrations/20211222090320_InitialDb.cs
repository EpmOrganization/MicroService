using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPM.DepartmentMicroService.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
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
                    Name = table.Column<string>(nullable: false),
                    ParentID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<int>(nullable: false),
                    Dep = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ClusterID);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ClusterID", "CreateTime", "CreateUser", "Dep", "Description", "ID", "IsDeleted", "Name", "ParentID", "UpdateTime", "UpdateUser" },
                values: new object[] { 1, new DateTime(2021, 12, 22, 17, 3, 19, 985, DateTimeKind.Local).AddTicks(8540), "系统管理员", null, null, new Guid("2751f652-4b4f-4484-8077-db3c35c6bcb2"), 0, "技术中心", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2021, 12, 22, 17, 3, 19, 985, DateTimeKind.Local).AddTicks(8853), "系统管理员" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
