using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    public partial class categoryrefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CatLangs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CatLangs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "CatLangs",
                type: "longtext",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "CatLangs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CatLangs",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CatLangs",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
