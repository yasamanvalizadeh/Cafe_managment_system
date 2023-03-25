using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeManagmentSystem.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class cafe103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "Orders");
        }
    }
}
