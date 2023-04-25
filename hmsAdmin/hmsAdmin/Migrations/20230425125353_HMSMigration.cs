using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hmsAdmin.Migrations
{
    /// <inheritdoc />
    public partial class HMSMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    app_price = table.Column<int>(type: "int", nullable: false),
                    dr_number = table.Column<int>(type: "int", nullable: true),
                    ns_number = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__departme__3213E83FBF388771", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    NID = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    image = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__patients__3213E83F6DEFA952", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    method = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__payment__3213E83FC7C36133", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pharmacy",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    med_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    cost = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pharmacy__3213E83FD9C3245B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__status__3213E83FAC4D67DD", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "timetable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    day_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    day_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__timetabl__3213E83FD8E7D805", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    fk_dept = table.Column<int>(type: "int", nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    experience = table.Column<int>(type: "int", nullable: false),
                    personal_profile = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    image = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fk_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__doctors__3213E83F918D10AD", x => x.id);
                    table.ForeignKey(
                        name: "FK_doctors_departments",
                        column: x => x.fk_dept,
                        principalTable: "departments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_doctors_status",
                        column: x => x.fk_status,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "nurses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    fk_dept = table.Column<int>(type: "int", nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    image = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fk_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__nurses__3213E83F13C92838", x => x.id);
                    table.ForeignKey(
                        name: "FK_nurses_departments",
                        column: x => x.fk_dept,
                        principalTable: "departments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_nurses_status",
                        column: x => x.fk_status,
                        principalTable: "status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    fk_pat = table.Column<int>(type: "int", nullable: true),
                    fk_dept = table.Column<int>(type: "int", nullable: true),
                    fk_tt = table.Column<int>(type: "int", nullable: true),
                    fk_pay = table.Column<int>(type: "int", nullable: true),
                    fk_stat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__appointm__3213E83F33B6A52E", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointments_departments",
                        column: x => x.fk_dept,
                        principalTable: "departments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_appointments_patients",
                        column: x => x.fk_pat,
                        principalTable: "patients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_appointments_payment",
                        column: x => x.fk_pay,
                        principalTable: "payment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_appointments_status",
                        column: x => x.fk_stat,
                        principalTable: "status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_appointments_timetable",
                        column: x => x.fk_tt,
                        principalTable: "timetable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "timedept",
                columns: table => new
                {
                    fk_dept = table.Column<int>(type: "int", nullable: true),
                    fk_time = table.Column<int>(type: "int", nullable: true),
                    available_dr = table.Column<int>(type: "int", nullable: false),
                    available_app = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_timedept_departments",
                        column: x => x.fk_dept,
                        principalTable: "departments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_timedept_timetable",
                        column: x => x.fk_time,
                        principalTable: "timetable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "diagnose",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    fk_app = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    prescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    analysis = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__diagnose__3213E83F7B00B9D0", x => x.id);
                    table.ForeignKey(
                        name: "FK_diagnose_appointments",
                        column: x => x.fk_app,
                        principalTable: "appointments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "lab",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    fk_app = table.Column<int>(type: "int", nullable: true),
                    ana_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    result = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: false),
                    cost = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__lab__3213E83F64C88213", x => x.id);
                    table.ForeignKey(
                        name: "FK_lab_appointments",
                        column: x => x.fk_app,
                        principalTable: "appointments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "medicine",
                columns: table => new
                {
                    fk_dia = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_medicine_diagnose",
                        column: x => x.fk_dia,
                        principalTable: "diagnose",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_fk_dept",
                table: "appointments",
                column: "fk_dept");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_fk_pat",
                table: "appointments",
                column: "fk_pat");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_fk_pay",
                table: "appointments",
                column: "fk_pay");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_fk_stat",
                table: "appointments",
                column: "fk_stat");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_fk_tt",
                table: "appointments",
                column: "fk_tt");

            migrationBuilder.CreateIndex(
                name: "IX_diagnose_fk_app",
                table: "diagnose",
                column: "fk_app");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_fk_dept",
                table: "doctors",
                column: "fk_dept");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_fk_status",
                table: "doctors",
                column: "fk_status");

            migrationBuilder.CreateIndex(
                name: "IX_lab_fk_app",
                table: "lab",
                column: "fk_app");

            migrationBuilder.CreateIndex(
                name: "IX_medicine_fk_dia",
                table: "medicine",
                column: "fk_dia");

            migrationBuilder.CreateIndex(
                name: "IX_nurses_fk_dept",
                table: "nurses",
                column: "fk_dept");

            migrationBuilder.CreateIndex(
                name: "IX_nurses_fk_status",
                table: "nurses",
                column: "fk_status");

            migrationBuilder.CreateIndex(
                name: "UQ__patients__C7DEC332A91E8FE2",
                table: "patients",
                column: "NID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_timedept_fk_dept",
                table: "timedept",
                column: "fk_dept");

            migrationBuilder.CreateIndex(
                name: "IX_timedept_fk_time",
                table: "timedept",
                column: "fk_time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "lab");

            migrationBuilder.DropTable(
                name: "medicine");

            migrationBuilder.DropTable(
                name: "nurses");

            migrationBuilder.DropTable(
                name: "pharmacy");

            migrationBuilder.DropTable(
                name: "timedept");

            migrationBuilder.DropTable(
                name: "diagnose");

            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "timetable");
        }
    }
}
