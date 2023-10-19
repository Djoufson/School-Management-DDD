using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingTheSeatsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolClassStudent");

            migrationBuilder.AddColumn<string>(
                name: "SchoolClassId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<string>(type: "TEXT", nullable: true),
                    ClassId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_SchoolClass_ClassId",
                        column: x => x.ClassId,
                        principalTable: "SchoolClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SchoolClassId",
                table: "Users",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_ClassId",
                table: "Seats",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_StudentId",
                table: "Seats",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SchoolClass_SchoolClassId",
                table: "Users",
                column: "SchoolClassId",
                principalTable: "SchoolClass",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SchoolClass_SchoolClassId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Users_SchoolClassId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SchoolClassId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "SchoolClassStudent",
                columns: table => new
                {
                    ClassesId = table.Column<string>(type: "TEXT", nullable: false),
                    StudentsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClassStudent", x => new { x.ClassesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_SchoolClassStudent_SchoolClass_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "SchoolClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolClassStudent_Users_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassStudent_StudentsId",
                table: "SchoolClassStudent",
                column: "StudentsId");
        }
    }
}
