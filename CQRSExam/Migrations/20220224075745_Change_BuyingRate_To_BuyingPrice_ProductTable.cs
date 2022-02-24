using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CQRSExam.Migrations
{
    public partial class Change_BuyingRate_To_BuyingPrice_ProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyingRate",
                table: "Products",
                newName: "BuyingPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyingPrice",
                table: "Products",
                newName: "BuyingRate");
        }
    }
}
