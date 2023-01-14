using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASMWeb.DataAccess.Migrations
{
    public partial class extendIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASM_Staffs_ASM_AppRole_RoleID",
                table: "ASM_Staffs");

            migrationBuilder.DropIndex(
                name: "IX_ASM_Staffs_RoleID",
                table: "ASM_Staffs");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "ASM_Employees",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "ASM_Employees");

            migrationBuilder.CreateIndex(
                name: "IX_ASM_Staffs_RoleID",
                table: "ASM_Staffs",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_ASM_Staffs_ASM_AppRole_RoleID",
                table: "ASM_Staffs",
                column: "RoleID",
                principalTable: "ASM_AppRole",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
