using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageFarm.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Crop = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Field__3214EC0700A8E46B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true, defaultValueSql: "(NULL)"),
                    ContactInfo = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true, defaultValueSql: "(NULL)"),
                    HourlyWage = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValueSql: "(NULL)"),
                    Role = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Staff__3214EC07F67B145D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true, defaultValueSql: "(NULL)"),
                    NextTestDate = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(NULL)"),
                    Status = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true, defaultValueSql: "(NULL)"),
                    FieldId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Machine__3214EC07FBCF565C", x => x.Id);
                    table.ForeignKey(
                        name: "Machine_Field_Id",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true, defaultValueSql: "(NULL)"),
                    FieldId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    MachineId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    StaffId = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Task__3214EC07166E2760", x => x.Id);
                    table.ForeignKey(
                        name: "Task_Field_Id",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Task_Machine_Id",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Task_Staff_Id",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Machine_FieldId",
                table: "Machine",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_FieldId",
                table: "Task",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_MachineId",
                table: "Task",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_StaffId",
                table: "Task",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Field");
        }
    }
}
