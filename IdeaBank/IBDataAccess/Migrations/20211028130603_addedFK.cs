using Microsoft.EntityFrameworkCore.Migrations;

namespace IBDataAccessLib.Migrations
{
    public partial class addedFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessUnitId",
                table: "Ideas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Ideas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_BusinessUnitId",
                table: "Ideas",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_DepartmentId",
                table: "Ideas",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_BusinessUnits_BusinessUnitId",
                table: "Ideas",
                column: "BusinessUnitId",
                principalTable: "BusinessUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Departments_DepartmentId",
                table: "Ideas",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_BusinessUnits_BusinessUnitId",
                table: "Ideas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Departments_DepartmentId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_BusinessUnitId",
                table: "Ideas");

            migrationBuilder.DropIndex(
                name: "IX_Ideas_DepartmentId",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "BusinessUnitId",
                table: "Ideas");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Ideas");
        }
    }
}
