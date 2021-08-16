namespace SecondHandClothes.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;
    public partial class OrderTableExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProductTitle",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductTitle",
                table: "Orders");
        }
    }
}
