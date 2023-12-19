using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class deneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AppUsers_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AppUsers_UserId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites");
        }
    }
}
