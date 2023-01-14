using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASMWeb.DataAccess.Migrations
{
    public partial class add_AppModel_toDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASM_AppModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppsID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ModelID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TypeCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Owners = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_AppModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASM_AppModels");
        }
    }
}
