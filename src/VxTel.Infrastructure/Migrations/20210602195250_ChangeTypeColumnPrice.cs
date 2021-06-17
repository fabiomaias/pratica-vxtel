using Microsoft.EntityFrameworkCore.Migrations;

namespace VxTel.Infrastructure.Migrations
{
    public partial class ChangeTypeColumnPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Origin",
                table: "Prices",
                type: "varchar",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Prices",
                type: "varchar",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldMaxLength: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Origin",
                table: "Prices",
                type: "INTEGER",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<int>(
                name: "Destination",
                table: "Prices",
                type: "INTEGER",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 3);
        }
    }
}
