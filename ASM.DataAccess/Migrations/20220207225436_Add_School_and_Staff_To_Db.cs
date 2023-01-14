using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM.DataAccess.Migrations
{
    public partial class Add_School_and_Staff_To_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASM_AppRole",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_AppRole", x => x.RoleID);
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
                    UnitID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Employees", x => x.Id);
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
                name: "ASM_Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RoleID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Staffs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASM_AppRole");

            migrationBuilder.DropTable(
                name: "ASM_Employees");

            migrationBuilder.DropTable(
                name: "ASM_Schools");

            migrationBuilder.DropTable(
                name: "ASM_Staffs");
        }
    }
}
