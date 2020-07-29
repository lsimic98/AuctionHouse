using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionHouse.Migrations
{
    public partial class UserStateDefaultActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fd3bf72-0674-4cfc-b83f-628e404b4a08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ac63ba7-c328-44f1-9b43-23f8d1ac993b");

            migrationBuilder.AlterColumn<string>(
                name: "state",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3268f411-7684-4e7d-b321-7da7dc4891ad", "dc09aafe-606e-40ba-8239-241bff54de4f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37b110d9-a4d4-47e0-be1e-6faba38fb963", "c946abb9-b811-4849-bfb0-c98f8f82ee51", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3268f411-7684-4e7d-b321-7da7dc4891ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b110d9-a4d4-47e0-be1e-6faba38fb963");

            migrationBuilder.AlterColumn<string>(
                name: "state",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "Active");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4fd3bf72-0674-4cfc-b83f-628e404b4a08", "8769ec63-ebc2-4e26-a39b-0265f2eb618f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ac63ba7-c328-44f1-9b43-23f8d1ac993b", "e31861c1-d040-4317-b065-b4382b2a8590", "Administrator", "ADMINISTRATOR" });
        }
    }
}
