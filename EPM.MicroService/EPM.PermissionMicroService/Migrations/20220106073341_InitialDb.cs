using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPM.PermissionMicroService.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
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
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ParentMenuID = table.Column<Guid>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<int>(nullable: false),
                    ParentList = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.ClusterID);
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "ClusterID", "CreateTime", "CreateUser", "Description", "ID", "IsDeleted", "Name", "ParentList", "ParentMenuID", "Type", "UpdateTime", "UpdateUser", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 6, 15, 33, 41, 364, DateTimeKind.Local).AddTicks(5250), "系统管理员", null, new Guid("ea22270d-fe74-4747-bca9-b97fd72033d5"), 0, "系统管理", null, null, null, new DateTime(2022, 1, 6, 15, 33, 41, 364, DateTimeKind.Local).AddTicks(5948), "系统管理员", null },
                    { 2, new DateTime(2022, 1, 6, 15, 33, 41, 364, DateTimeKind.Local).AddTicks(6569), "系统管理员", null, new Guid("daa375e9-8761-4204-9560-5890146a9912"), 0, "用户管理", "ea22270d-fe74-4747-bca9-b97fd72033d5", new Guid("ea22270d-fe74-4747-bca9-b97fd72033d5"), null, new DateTime(2022, 1, 6, 15, 33, 41, 364, DateTimeKind.Local).AddTicks(6576), "系统管理员", null },
                    { 3, new DateTime(2022, 1, 6, 15, 33, 41, 364, DateTimeKind.Local).AddTicks(7485), "系统管理员", null, new Guid("3900326c-3deb-4f97-bce2-1dc0c4647d96"), 0, "角色管理", "ea22270d-fe74-4747-bca9-b97fd72033d5", new Guid("ea22270d-fe74-4747-bca9-b97fd72033d5"), null, new DateTime(2022, 1, 6, 15, 33, 41, 364, DateTimeKind.Local).AddTicks(7486), "系统管理员", null },
                    { 4, new DateTime(2022, 1, 6, 15, 33, 41, 364, DateTimeKind.Local).AddTicks(7512), "系统管理员", null, new Guid("0ec57811-7075-4817-8e16-54ad827a0557"), 0, "部门管理", "ea22270d-fe74-4747-bca9-b97fd72033d5", new Guid("ea22270d-fe74-4747-bca9-b97fd72033d5"), null, new DateTime(2022, 1, 6, 15, 33, 41, 364, DateTimeKind.Local).AddTicks(7513), "系统管理员", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
