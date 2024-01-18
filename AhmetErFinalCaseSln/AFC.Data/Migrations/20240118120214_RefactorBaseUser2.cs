using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorBaseUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreateAt", "LastActivityDate", "LastName", "Password" },
                values: new object[] { new DateTime(2024, 1, 18, 15, 2, 14, 395, DateTimeKind.Local).AddTicks(6930), new DateTime(2024, 1, 18, 15, 2, 14, 395, DateTimeKind.Local).AddTicks(6932), "Admin2", "233bdb0dca0cfc12b4b01bcfbd553574" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateAt", "LastActivityDate" },
                values: new object[] { new DateTime(2024, 1, 18, 15, 0, 45, 283, DateTimeKind.Local).AddTicks(4021), new DateTime(2024, 1, 18, 15, 0, 45, 283, DateTimeKind.Local).AddTicks(4034) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateAt", "LastActivityDate", "LastName", "Password" },
                values: new object[] { new DateTime(2024, 1, 18, 15, 0, 45, 283, DateTimeKind.Local).AddTicks(4342), new DateTime(2024, 1, 18, 15, 0, 45, 283, DateTimeKind.Local).AddTicks(4343), "233bdb0dca0cfc12b4b01bcfbd553574", "Admin2!" });
        }
    }
}
