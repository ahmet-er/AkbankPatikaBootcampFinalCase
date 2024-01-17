using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "PaymentCategory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    LastActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordRetryCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldStaff",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldStaff_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseRequest",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldStaffId = table.Column<int>(type: "int", nullable: false),
                    PaymentCategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    PaymentLocation = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ExpenseStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseRequest_FieldStaff_FieldStaffId",
                        column: x => x.FieldStaffId,
                        principalSchema: "dbo",
                        principalTable: "FieldStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseRequest_PaymentCategory_PaymentCategoryId",
                        column: x => x.PaymentCategoryId,
                        principalSchema: "dbo",
                        principalTable: "PaymentCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "CreateAt", "CreateBy", "Email", "FirstName", "LastActivityDate", "LastName", "ModifiedAt", "ModifiedBy", "Password", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 17, 11, 47, 11, 175, DateTimeKind.Local).AddTicks(995), 1, "admin1@gmail.com", "Admin", new DateTime(2024, 1, 17, 11, 47, 11, 175, DateTimeKind.Local).AddTicks(1009), "Admin1", null, null, "Admin1!", 0, "admin1" },
                    { 2, new DateTime(2024, 1, 17, 11, 47, 11, 175, DateTimeKind.Local).AddTicks(1012), 1, "admin2@gmail.com", "Admin", new DateTime(2024, 1, 17, 11, 47, 11, 175, DateTimeKind.Local).AddTicks(1013), "Admin2", null, null, "Admin2!", 0, "admin2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseRequest_FieldStaffId",
                schema: "dbo",
                table: "ExpenseRequest",
                column: "FieldStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseRequest_PaymentCategoryId",
                schema: "dbo",
                table: "ExpenseRequest",
                column: "PaymentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldStaff_UserId",
                schema: "dbo",
                table: "FieldStaff",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "dbo",
                table: "User",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseRequest",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FieldStaff",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PaymentCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
