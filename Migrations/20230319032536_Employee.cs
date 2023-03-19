using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDLC.Migrations
{
    public partial class Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(nullable: false),
                    DesignationName = table.Column<string>(nullable: true),
                    DepartmentName = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    DesignationId = table.Column<int>(nullable: false),
                    BasicSalary = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
