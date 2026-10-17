using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealMate.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderRefrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderReference",
                table: "Orders",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UX_Orders_OrderReference",
                table: "Orders",
                column: "OrderReference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_Orders_OrderReference",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderReference",
                table: "Orders");
        }
    }
}
