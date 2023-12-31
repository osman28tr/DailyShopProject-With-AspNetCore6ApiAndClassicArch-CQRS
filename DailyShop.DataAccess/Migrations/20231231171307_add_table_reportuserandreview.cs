using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DailyShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_table_reportuserandreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ReportReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<int>(type: "int", nullable: true),
                    ReporterReviewId = table.Column<int>(type: "int", nullable: true),
                    ReportedMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportReviews_Reviews_ReporterReviewId",
                        column: x => x.ReporterReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportReviews_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ReporterUserId = table.Column<int>(type: "int", nullable: true),
                    ReportedMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportUsers_AppUsers_ReporterUserId",
                        column: x => x.ReporterUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportUsers_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportReviews_ReporterReviewId",
                table: "ReportReviews",
                column: "ReporterReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportReviews_ReviewId",
                table: "ReportReviews",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportUsers_ReporterUserId",
                table: "ReportUsers",
                column: "ReporterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportUsers_UserId",
                table: "ReportUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportReviews");

            migrationBuilder.DropTable(
                name: "ReportUsers");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BodyImage", "CategoryId", "CreatedAt", "Description", "IsApproved", "IsDeleted", "Name", "Price", "Rating", "Status", "Stock", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 31, "productimagess", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4524), "denemedes1", true, false, "deneme1", 140m, (byte)6, "yeni", 12, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4534), 1 },
                    { 32, "productimagess2", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4540), "denemedes1", true, false, "deneme2", 123m, (byte)2, "yeni", 13, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4541), 1 },
                    { 33, "productimagess3", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4542), "denemedes1", true, false, "deneme3", 140m, (byte)3, "yeni", 14, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4542), 1 },
                    { 34, "productimagess4", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4543), "denemedes1", true, false, "deneme4", 124m, (byte)4, "yeni", 126, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4544), 1 },
                    { 35, "productimagess5", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4545), "denemedes1", true, false, "deneme5", 126m, (byte)5, "yeni", 127, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4545), 1 },
                    { 36, "productimagess6", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4546), "denemedes1", true, false, "deneme6", 167m, (byte)6, "yeni", 129, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4547), 1 },
                    { 37, "productimagess7", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4548), "denemedes1", true, false, "deneme7", 156m, (byte)6, "yeni", 12, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4548), 2 },
                    { 38, "productimagess8", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4549), "denemedes1", true, false, "deneme8", 145m, (byte)6, "yeni", 15, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4549), 2 },
                    { 39, "productimagess9", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4554), "denemedes1", true, false, "deneme9", 167m, (byte)5, "yeni", 12, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4554), 2 },
                    { 40, "productimagess10", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4556), "denemedes1", true, false, "deneme10", 140m, (byte)6, "yeni", 21, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4556), 2 },
                    { 41, "productimagess11", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4557), "denemedes1", true, false, "deneme11", 170m, (byte)7, "yeni", 12, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4557), 2 },
                    { 42, "productimagess12", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4558), "denemedes1", true, false, "deneme12", 190m, (byte)6, "yeni", 24, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4559), 2 },
                    { 43, "productimagess13", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4560), "denemedes1", true, false, "deneme13", 167m, (byte)2, "yeni", 126, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4560), 2 },
                    { 44, "productimagess14", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4561), "denemedes1", true, false, "deneme14", 156m, (byte)1, "yeni", 272, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4561), 2 },
                    { 45, "productimagess15", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4562), "denemedes1", true, false, "deneme15", 145m, (byte)6, "yeni", 30, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4563), 2 },
                    { 46, "productimagess16", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4564), "denemedes1", true, false, "deneme16", 167m, (byte)3, "yeni", 31, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4564), 2 },
                    { 47, "productimagess17", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4565), "denemedes1", true, false, "deneme17", 178m, (byte)6, "yeni", 36, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4565), 2 },
                    { 48, "productimagess18", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4566), "denemedes1", true, false, "deneme18", 179m, (byte)6, "yeni", 58, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4567), 2 },
                    { 49, "productimagess19", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4568), "denemedes1", true, false, "deneme19", 145m, (byte)2, "yeni", 78, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4568), 2 },
                    { 50, "productimagess20", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4569), "denemedes1", true, false, "deneme20", 134m, (byte)6, "yeni", 57, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4569), 2 },
                    { 51, "productimagess21", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4570), "denemedes1", true, false, "deneme21", 145m, (byte)3, "yeni", 46, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4571), 2 },
                    { 52, "productimagess22", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4572), "denemedes1", true, false, "deneme22", 167m, (byte)6, "yeni", 89, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4572), 1 },
                    { 53, "productimagess23", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4573), "denemedes1", true, false, "deneme23", 189m, (byte)4, "yeni", 57, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4573), 1 },
                    { 54, "productimagess24", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4574), "denemedes1", true, false, "deneme24", 190m, (byte)6, "yeni", 38, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4575), 1 },
                    { 55, "productimagess25", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4577), "denemedes1", true, false, "deneme25", 145m, (byte)5, "yeni", 77, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4577), 3 },
                    { 56, "productimagess26", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4578), "denemedes1", true, false, "deneme26", 123m, (byte)6, "yeni", 96, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4579), 3 },
                    { 57, "productimagess27", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4580), "denemedes1", true, false, "deneme27", 112m, (byte)1, "yeni", 65, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4580), 3 },
                    { 58, "productimagess28", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4581), "denemedes1", true, false, "deneme28", 145m, (byte)3, "yeni", 67, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4581), 3 },
                    { 59, "productimagess29", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4582), "denemedes1", true, false, "deneme29", 167m, (byte)7, "yeni", 47, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4583), 3 },
                    { 60, "productimagess30", 1, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4584), "denemedes1", true, false, "deneme30", 178m, (byte)6, "yeni", 98, new DateTime(2023, 12, 31, 0, 54, 22, 404, DateTimeKind.Local).AddTicks(4584), 3 }
                });
        }
    }
}
