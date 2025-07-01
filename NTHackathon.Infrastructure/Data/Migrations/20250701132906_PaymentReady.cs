using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTHackathon.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class PaymentReady : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_paid",
                table: "reservation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "payment_id",
                table: "reservation",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_paid",
                table: "reservation");

            migrationBuilder.DropColumn(
                name: "payment_id",
                table: "reservation");
        }
    }
}
