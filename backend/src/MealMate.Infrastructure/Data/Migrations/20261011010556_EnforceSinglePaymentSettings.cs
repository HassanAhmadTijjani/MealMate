using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealMate.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnforceSinglePaymentSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SettingsKey",
                table: "PaymentSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "UX_PaymentSettings_SettingsKey",
                table: "PaymentSettings",
                column: "SettingsKey",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_PaymentSettings_SettingsKey",
                table: "PaymentSettings",
                sql: "\"SettingsKey\" = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_PaymentSettings_SettingsKey",
                table: "PaymentSettings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_PaymentSettings_SettingsKey",
                table: "PaymentSettings");

            migrationBuilder.DropColumn(
                name: "SettingsKey",
                table: "PaymentSettings");
        }
    }
}
