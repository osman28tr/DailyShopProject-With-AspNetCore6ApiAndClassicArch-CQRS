using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DailyShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class drop_column_deneme_in_product_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DenemeSütunu",
                table: "Products");           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.AddColumn<string>(
                name: "DenemeSütunu",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
