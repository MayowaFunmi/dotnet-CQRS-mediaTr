using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRSMediaTr.Migrations
{
    /// <inheritdoc />
    public partial class Fingerprintmodel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FingerprintTemplate",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FingerprintTemplate",
                table: "AspNetUsers");
        }
    }
}
