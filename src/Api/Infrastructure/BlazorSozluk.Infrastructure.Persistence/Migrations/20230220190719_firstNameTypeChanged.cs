using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorSozluk.Infrastructure.Persistence.Migrations
{
    public partial class firstNameTypeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "dbo",
                table: "user",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FirstName",
                schema: "dbo",
                table: "user",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
