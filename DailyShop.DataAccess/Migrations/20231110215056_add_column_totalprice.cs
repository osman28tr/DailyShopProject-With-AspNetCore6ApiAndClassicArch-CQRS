using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_column_totalprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartItems");
        }
    }
}
