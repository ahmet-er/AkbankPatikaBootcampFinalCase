using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorExpenseRequestConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanyResultDescription",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "LastActivityDate" },
                values: new object[] { new DateTime(2024, 1, 19, 1, 14, 41, 911, DateTimeKind.Local).AddTicks(7395), new DateTime(2024, 1, 19, 1, 14, 41, 911, DateTimeKind.Local).AddTicks(7407) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "LastActivityDate" },
                values: new object[] { new DateTime(2024, 1, 19, 1, 14, 41, 911, DateTimeKind.Local).AddTicks(7802), new DateTime(2024, 1, 19, 1, 14, 41, 911, DateTimeKind.Local).AddTicks(7804) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanyResultDescription",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "LastActivityDate" },
                values: new object[] { new DateTime(2024, 1, 18, 15, 2, 14, 395, DateTimeKind.Local).AddTicks(6552), new DateTime(2024, 1, 18, 15, 2, 14, 395, DateTimeKind.Local).AddTicks(6564) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "LastActivityDate" },
                values: new object[] { new DateTime(2024, 1, 18, 15, 2, 14, 395, DateTimeKind.Local).AddTicks(6930), new DateTime(2024, 1, 18, 15, 2, 14, 395, DateTimeKind.Local).AddTicks(6932) });
        }
    }
}
