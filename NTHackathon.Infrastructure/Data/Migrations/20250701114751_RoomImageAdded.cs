using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTHackathon.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoomImageAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "room",
                type: "character varying(2049)",
                maxLength: 2049,
                nullable: false,
                defaultValue: "about:blank");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                table: "room");
        }
    }
}
