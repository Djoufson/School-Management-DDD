using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingColumnsOnTableSchoolClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Users_StudentId",
                table: "Seats");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Seats",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeats",
                table: "SchoolClass",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSeats",
                table: "SchoolClass",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Users_StudentId",
                table: "Seats",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Users_StudentId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "AvailableSeats",
                table: "SchoolClass");

            migrationBuilder.DropColumn(
                name: "TotalSeats",
                table: "SchoolClass");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Seats",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Users_StudentId",
                table: "Seats",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
