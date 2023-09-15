using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoucherId",
                table: "Bid",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bid_VoucherId",
                table: "Bid",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Vouchers_VoucherId",
                table: "Bid",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Vouchers_VoucherId",
                table: "Bid");

            migrationBuilder.DropIndex(
                name: "IX_Bid_VoucherId",
                table: "Bid");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "Bid");
        }
    }
}
