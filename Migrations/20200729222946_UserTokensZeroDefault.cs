using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionHouse.Migrations
{
    public partial class UserTokensZeroDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2213c160-466f-4aac-97e5-d9e9e694deef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "775ec7a3-7c0e-4ead-896f-4a4cad239a83");

            migrationBuilder.AlterColumn<int>(
                name: "tokens",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4fd3bf72-0674-4cfc-b83f-628e404b4a08", "8769ec63-ebc2-4e26-a39b-0265f2eb618f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ac63ba7-c328-44f1-9b43-23f8d1ac993b", "e31861c1-d040-4317-b065-b4382b2a8590", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fd3bf72-0674-4cfc-b83f-628e404b4a08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ac63ba7-c328-44f1-9b43-23f8d1ac993b");

            migrationBuilder.AlterColumn<int>(
                name: "tokens",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "775ec7a3-7c0e-4ead-896f-4a4cad239a83", "8b50caa8-052f-48ad-a8ed-06f22df91d75", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2213c160-466f-4aac-97e5-d9e9e694deef", "e9d13f48-e799-4621-afc6-a95962f6fc30", "Administrator", "ADMINISTRATOR" });
        }
    }
}
