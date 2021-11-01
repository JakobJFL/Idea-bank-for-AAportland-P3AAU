using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseLib.Migrations
{
    public partial class FkAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitRefId",
                table: "IdeasTbl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsTblRefId",
                table: "IdeasTbl",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessUnitRefId",
                table: "IdeasTbl");

            migrationBuilder.DropColumn(
                name: "DepartmentsTblRefId",
                table: "IdeasTbl");
        }
    }
}
