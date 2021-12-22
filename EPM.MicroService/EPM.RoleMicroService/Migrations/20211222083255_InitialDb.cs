using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPM.RoleMicroService.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
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
                    IsDeleted = table.Column<int>(nullable: false),
                    HalfCheckeds = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ClusterID);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ClusterID", "CreateTime", "CreateUser", "Description", "HalfCheckeds", "ID", "IsDeleted", "Name", "UpdateTime", "UpdateUser" },
                values: new object[] { 1, new DateTime(2021, 12, 22, 16, 32, 54, 648, DateTimeKind.Local).AddTicks(1976), "系统管理员", null, null, new Guid("d4c6bc89-d592-4ea6-94ff-0ebf9999892c"), 0, "系统管理员", new DateTime(2021, 12, 22, 16, 32, 54, 648, DateTimeKind.Local).AddTicks(2277), "系统管理员" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
