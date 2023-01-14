using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM.DataAccess.Migrations
{
    public partial class Add_Apps_and_Models_To_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASM_AppModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ModelID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeveloperId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TypeCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Owners = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_AppModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASM_Apps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AppName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DeveloperId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Owners = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AppUrlTest = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AppUrlTrain = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASM_Apps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASM_AppModels");

            migrationBuilder.DropTable(
                name: "ASM_Apps");
        }
    }
}
