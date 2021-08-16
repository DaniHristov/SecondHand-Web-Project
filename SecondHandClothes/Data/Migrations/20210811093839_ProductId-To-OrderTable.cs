namespace SecondHandClothes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ProductIdToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Orders");
        }
    }
}
