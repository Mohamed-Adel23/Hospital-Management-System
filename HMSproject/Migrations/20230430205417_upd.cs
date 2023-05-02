using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMSproject.Migrations
{
    /// <inheritdoc />
    public partial class upd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_FkPatNavigationId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Departments_FkDeptNavigationId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_FkDeptNavigationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "FkDeptNavigationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "fk_dept",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "fk_pat",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "FkPatNavigationId",
                table: "Appointments",
                newName: "PatientID");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_FkPatNavigationId",
                table: "Appointments",
                newName: "IX_Appointments_PatientID");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DepartmentID",
                table: "Appointments",
                column: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientID",
                table: "Appointments",
                column: "PatientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Departments_DepartmentID",
                table: "Appointments",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Departments_DepartmentID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DepartmentID",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Appointments",
                newName: "FkPatNavigationId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientID",
                table: "Appointments",
                newName: "IX_Appointments_FkPatNavigationId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "FkDeptNavigationId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "fk_dept",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fk_pat",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_FkDeptNavigationId",
                table: "Appointments",
                column: "FkDeptNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_FkPatNavigationId",
                table: "Appointments",
                column: "FkPatNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Departments_FkDeptNavigationId",
                table: "Appointments",
                column: "FkDeptNavigationId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
