using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    public partial class postrefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Posts",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Posts",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Posts",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "longtext",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Posts",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
