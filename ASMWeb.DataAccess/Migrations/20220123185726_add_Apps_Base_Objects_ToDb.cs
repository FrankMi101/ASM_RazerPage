using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASMWeb.DataAccess.Migrations
{
    public partial class add_Apps_Base_Objects_ToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASM_Apps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AppName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owners = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AppUrlTest = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AppUrlTrain = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_Developers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DistrictName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Emplpyees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_GrantTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_GrantTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    PermissionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_Principals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PrincipalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Principals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_SchoolAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AreaName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SupervisorID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OfficerID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_SchoolAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_Schools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BSID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    BriefName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PrincipalID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AreaCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DistrictCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_SchoolTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_SchoolTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_Scopes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    AccessScope = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Scopes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASM_Apps");

            migrationBuilder.DropTable(
                name: "ASM_Developers");

            migrationBuilder.DropTable(
                name: "ASM_Districts");

            migrationBuilder.DropTable(
                name: "ASM_Employees");

            migrationBuilder.DropTable(
                name: "ASM_GrantTypes");

            migrationBuilder.DropTable(
                name: "ASM_Permissions");

            migrationBuilder.DropTable(
                name: "ASM_Principals");

            migrationBuilder.DropTable(
                name: "ASM_SchoolAreas");

            migrationBuilder.DropTable(
                name: "ASM_Schools");

            migrationBuilder.DropTable(
                name: "ASM_SchoolTypes");

            migrationBuilder.DropTable(
                name: "ASM_Scopes");
        }
    }
}
