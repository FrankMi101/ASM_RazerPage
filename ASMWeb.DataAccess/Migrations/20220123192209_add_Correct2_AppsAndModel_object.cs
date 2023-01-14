using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASMWeb.DataAccess.Migrations
{
    public partial class add_Correct2_AppsAndModel_object : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Developer",
                table: "ASM_Apps");

            migrationBuilder.RenameColumn(
                name: "Developer",
                table: "ASM_AppModels",
                newName: "DeveloperId");

            migrationBuilder.RenameColumn(
                name: "AppsID",
                table: "ASM_AppModels",
                newName: "AppID");

            migrationBuilder.AddColumn<string>(
                name: "DeveloperId",
                table: "ASM_Apps",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "ASM_Apps");

            migrationBuilder.RenameColumn(
                name: "DeveloperId",
                table: "ASM_AppModels",
                newName: "Developer");

            migrationBuilder.RenameColumn(
                name: "AppID",
                table: "ASM_AppModels",
                newName: "AppsID");

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "ASM_Apps",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
