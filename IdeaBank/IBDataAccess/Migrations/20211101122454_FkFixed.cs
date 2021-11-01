using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseLib.Migrations
{
    public partial class FkFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentsTblRefId",
                table: "IdeasTbl",
                newName: "DepartmentsRefId");

            migrationBuilder.AddColumn<int>(
                name: "CommentsRefId",
                table: "IdeasTbl",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentsRefId",
                table: "IdeasTbl");

            migrationBuilder.RenameColumn(
                name: "DepartmentsRefId",
                table: "IdeasTbl",
                newName: "DepartmentsTblRefId");
        }
    }
}
