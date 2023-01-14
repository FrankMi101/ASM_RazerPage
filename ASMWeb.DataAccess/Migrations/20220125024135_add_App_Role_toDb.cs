using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASMWeb.DataAccess.Migrations
{
    public partial class add_App_Role_toDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASM_AppRole",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_AppRole", x => x.RoleID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASM_AppRole");
        }
    }
}
