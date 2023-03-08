using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    public partial class AddLanguageRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "PostLangs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PostLangs_LanguageId",
                table: "PostLangs",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CatLangs_LanguageId",
                table: "CatLangs",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatLangs_Languages_LanguageId",
                table: "CatLangs",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLangs_Languages_LanguageId",
                table: "PostLangs",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatLangs_Languages_LanguageId",
                table: "CatLangs");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLangs_Languages_LanguageId",
                table: "PostLangs");

            migrationBuilder.DropIndex(
                name: "IX_PostLangs_LanguageId",
                table: "PostLangs");

            migrationBuilder.DropIndex(
                name: "IX_CatLangs_LanguageId",
                table: "CatLangs");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "PostLangs");
        }
    }
}
