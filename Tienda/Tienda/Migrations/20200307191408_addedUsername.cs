using Microsoft.EntityFrameworkCore.Migrations;

namespace Tienda.Migrations
{
    public partial class addedUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Customers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Username",
                table: "Customers",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Username",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
