using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseLib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessUnitsTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnitsTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentsTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Initials = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Message = table.Column<string>(type: "varchar(1500)", maxLength: 1500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdeaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentsTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdeasTbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Risk = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Team = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PlanDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ExpectedResults = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeasTbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeasTbl_BusinessUnitsTbl_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnitsTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdeasTbl_DepartmentsTbl_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DepartmentsTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdeasTbl_BusinessUnitId",
                table: "IdeasTbl",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeasTbl_DepartmentId",
                table: "IdeasTbl",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentsTbl");

            migrationBuilder.DropTable(
                name: "IdeasTbl");

            migrationBuilder.DropTable(
                name: "BusinessUnitsTbl");

            migrationBuilder.DropTable(
                name: "DepartmentsTbl");
        }
    }
}
