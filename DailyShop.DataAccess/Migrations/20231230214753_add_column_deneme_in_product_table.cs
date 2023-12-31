using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DailyShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_column_deneme_in_product_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "DenemeSütunu",
            //    table: "Products",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BodyImage", "CategoryId", "CreatedAt", "DenemeSütunu", "Description", "IsApproved", "IsDeleted", "Name", "Price", "Rating", "Status", "Stock", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 31, "productimagess", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6210), null, "denemedes1", true, false, "deneme1", 140m, (byte)6, "yeni", 12, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6223), 1 },
                    { 32, "productimagess2", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6232), null, "denemedes1", true, false, "deneme2", 123m, (byte)2, "yeni", 13, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6232), 1 },
                    { 33, "productimagess3", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6235), null, "denemedes1", true, false, "deneme3", 140m, (byte)3, "yeni", 14, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6235), 1 },
                    { 34, "productimagess4", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6236), null, "denemedes1", true, false, "deneme4", 124m, (byte)4, "yeni", 126, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6237), 1 },
                    { 35, "productimagess5", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6238), null, "denemedes1", true, false, "deneme5", 126m, (byte)5, "yeni", 127, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6238), 1 },
                    { 36, "productimagess6", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6240), null, "denemedes1", true, false, "deneme6", 167m, (byte)6, "yeni", 129, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6240), 1 },
                    { 37, "productimagess7", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6241), null, "denemedes1", true, false, "deneme7", 156m, (byte)6, "yeni", 12, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6241), 2 },
                    { 38, "productimagess8", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6243), null, "denemedes1", true, false, "deneme8", 145m, (byte)6, "yeni", 15, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6243), 2 },
                    { 39, "productimagess9", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6244), null, "denemedes1", true, false, "deneme9", 167m, (byte)5, "yeni", 12, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6245), 2 },
                    { 40, "productimagess10", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6246), null, "denemedes1", true, false, "deneme10", 140m, (byte)6, "yeni", 21, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6246), 2 },
                    { 41, "productimagess11", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6247), null, "denemedes1", true, false, "deneme11", 170m, (byte)7, "yeni", 12, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6248), 2 },
                    { 42, "productimagess12", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6249), null, "denemedes1", true, false, "deneme12", 190m, (byte)6, "yeni", 24, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6249), 2 },
                    { 43, "productimagess13", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6250), null, "denemedes1", true, false, "deneme13", 167m, (byte)2, "yeni", 126, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6251), 2 },
                    { 44, "productimagess14", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6253), null, "denemedes1", true, false, "deneme14", 156m, (byte)1, "yeni", 272, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6254), 2 },
                    { 45, "productimagess15", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6255), null, "denemedes1", true, false, "deneme15", 145m, (byte)6, "yeni", 30, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6255), 2 },
                    { 46, "productimagess16", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6257), null, "denemedes1", true, false, "deneme16", 167m, (byte)3, "yeni", 31, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6257), 2 },
                    { 47, "productimagess17", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6258), null, "denemedes1", true, false, "deneme17", 178m, (byte)6, "yeni", 36, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6259), 2 },
                    { 48, "productimagess18", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6296), null, "denemedes1", true, false, "deneme18", 179m, (byte)6, "yeni", 58, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6297), 2 },
                    { 49, "productimagess19", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6298), null, "denemedes1", true, false, "deneme19", 145m, (byte)2, "yeni", 78, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6298), 2 },
                    { 50, "productimagess20", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6299), null, "denemedes1", true, false, "deneme20", 134m, (byte)6, "yeni", 57, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6300), 2 },
                    { 51, "productimagess21", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6301), null, "denemedes1", true, false, "deneme21", 145m, (byte)3, "yeni", 46, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6301), 2 },
                    { 52, "productimagess22", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6302), null, "denemedes1", true, false, "deneme22", 167m, (byte)6, "yeni", 89, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6303), 1 },
                    { 53, "productimagess23", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6304), null, "denemedes1", true, false, "deneme23", 189m, (byte)4, "yeni", 57, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6305), 1 },
                    { 54, "productimagess24", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6306), null, "denemedes1", true, false, "deneme24", 190m, (byte)6, "yeni", 38, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6306), 1 },
                    { 55, "productimagess25", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6307), null, "denemedes1", true, false, "deneme25", 145m, (byte)5, "yeni", 77, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6308), 3 },
                    { 56, "productimagess26", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6309), null, "denemedes1", true, false, "deneme26", 123m, (byte)6, "yeni", 96, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6309), 3 },
                    { 57, "productimagess27", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6310), null, "denemedes1", true, false, "deneme27", 112m, (byte)1, "yeni", 65, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6311), 3 },
                    { 58, "productimagess28", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6312), null, "denemedes1", true, false, "deneme28", 145m, (byte)3, "yeni", 67, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6313), 3 },
                    { 59, "productimagess29", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6314), null, "denemedes1", true, false, "deneme29", 167m, (byte)7, "yeni", 47, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6314), 3 },
                    { 60, "productimagess30", 1, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6315), null, "denemedes1", true, false, "deneme30", 178m, (byte)6, "yeni", 98, new DateTime(2023, 12, 31, 0, 47, 53, 98, DateTimeKind.Local).AddTicks(6316), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DropColumn(
                name: "DenemeSütunu",
                table: "Products");
        }
    }
}
