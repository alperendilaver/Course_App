using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTableOgretmenId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OgretmenId",
                table: "kursKayitlari",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_kursKayitlari_OgretmenId",
                table: "kursKayitlari",
                column: "OgretmenId");

            migrationBuilder.AddForeignKey(
                name: "FK_kursKayitlari_Ogretmenler_OgretmenId",
                table: "kursKayitlari",
                column: "OgretmenId",
                principalTable: "Ogretmenler",
                principalColumn: "OgretmenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_kursKayitlari_Ogretmenler_OgretmenId",
                table: "kursKayitlari");

            migrationBuilder.DropIndex(
                name: "IX_kursKayitlari_OgretmenId",
                table: "kursKayitlari");

            migrationBuilder.DropColumn(
                name: "OgretmenId",
                table: "kursKayitlari");
        }
    }
}
