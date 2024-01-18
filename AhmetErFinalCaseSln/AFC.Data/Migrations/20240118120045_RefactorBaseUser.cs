using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorBaseUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentPath",
                schema: "dbo",
                table: "ExpenseRequest");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "LastActivityDate", "Password" },
                values: new object[] { new DateTime(2024, 1, 18, 15, 0, 45, 283, DateTimeKind.Local).AddTicks(4021), new DateTime(2024, 1, 18, 15, 0, 45, 283, DateTimeKind.Local).AddTicks(4034), "944f48c8bfbb918750e6c243734d4b21" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "LastActivityDate", "LastName" },
                values: new object[] { new DateTime(2024, 1, 18, 15, 0, 45, 283, DateTimeKind.Local).AddTicks(4342), new DateTime(2024, 1, 18, 15, 0, 45, 283, DateTimeKind.Local).AddTicks(4343), "233bdb0dca0cfc12b4b01bcfbd553574" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                schema: "dbo",
                table: "ExpenseRequest",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "LastActivityDate", "Password" },
                values: new object[] { new DateTime(2024, 1, 17, 18, 40, 32, 194, DateTimeKind.Local).AddTicks(2446), new DateTime(2024, 1, 17, 18, 40, 32, 194, DateTimeKind.Local).AddTicks(2459), "Admin1!" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "LastActivityDate", "LastName" },
                values: new object[] { new DateTime(2024, 1, 17, 18, 40, 32, 194, DateTimeKind.Local).AddTicks(2462), new DateTime(2024, 1, 17, 18, 40, 32, 194, DateTimeKind.Local).AddTicks(2463), "Admin2" });
        }
    }
}
