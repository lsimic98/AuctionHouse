using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionHouse.Migrations
{
    public partial class GenderAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7685171e-58ec-4544-98e0-a5fc4beb27d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae846572-350a-4470-af8c-d913b32b7a3d");

            migrationBuilder.DropColumn(
                name: "sex",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "775ec7a3-7c0e-4ead-896f-4a4cad239a83", "8b50caa8-052f-48ad-a8ed-06f22df91d75", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2213c160-466f-4aac-97e5-d9e9e694deef", "e9d13f48-e799-4621-afc6-a95962f6fc30", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2213c160-466f-4aac-97e5-d9e9e694deef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "775ec7a3-7c0e-4ead-896f-4a4cad239a83");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "sex",
                table: "AspNetUsers",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae846572-350a-4470-af8c-d913b32b7a3d", "f6326f52-b26b-44d9-b6ef-1a6b543825df", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7685171e-58ec-4544-98e0-a5fc4beb27d3", "fa895699-55a1-4aac-9e50-2034b22bcc65", "Administrator", "ADMINISTRATOR" });
        }
    }
}
