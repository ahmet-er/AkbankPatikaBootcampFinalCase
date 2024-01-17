using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseDocumentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyResultDescription",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExpenseDocument",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseRequestId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseDocument_ExpenseRequest_ExpenseRequestId",
                        column: x => x.ExpenseRequestId,
                        principalSchema: "dbo",
                        principalTable: "ExpenseRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "LastActivityDate" },
                values: new object[] { new DateTime(2024, 1, 17, 18, 40, 32, 194, DateTimeKind.Local).AddTicks(2446), new DateTime(2024, 1, 17, 18, 40, 32, 194, DateTimeKind.Local).AddTicks(2459) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "LastActivityDate" },
                values: new object[] { new DateTime(2024, 1, 17, 18, 40, 32, 194, DateTimeKind.Local).AddTicks(2462), new DateTime(2024, 1, 17, 18, 40, 32, 194, DateTimeKind.Local).AddTicks(2463) });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDocument_ExpenseRequestId",
                schema: "dbo",
                table: "ExpenseDocument",
                column: "ExpenseRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseDocument",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "CompanyResultDescription",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "LastActivityDate" },
                values: new object[] { new DateTime(2024, 1, 17, 11, 47, 11, 175, DateTimeKind.Local).AddTicks(995), new DateTime(2024, 1, 17, 11, 47, 11, 175, DateTimeKind.Local).AddTicks(1009) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "LastActivityDate" },
                values: new object[] { new DateTime(2024, 1, 17, 11, 47, 11, 175, DateTimeKind.Local).AddTicks(1012), new DateTime(2024, 1, 17, 11, 47, 11, 175, DateTimeKind.Local).AddTicks(1013) });
        }
    }
}
