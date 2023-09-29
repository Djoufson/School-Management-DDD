using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingAdminRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClass_Users_AdminId",
                table: "SchoolClass");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "SchoolClass",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClass_Users_AdminId",
                table: "SchoolClass",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClass_Users_AdminId",
                table: "SchoolClass");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "SchoolClass",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClass_Users_AdminId",
                table: "SchoolClass",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
