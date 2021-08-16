namespace SecondHandClothes.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class OrderTableUpdateTownColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Town",
                table: "Orders");
        }
    }
}
