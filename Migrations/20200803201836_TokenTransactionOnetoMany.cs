using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionHouse.Migrations
{
    public partial class TokenTransactionOnetoMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenTransactions_BagTokens_bagId",
                table: "TokenTransactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3268f411-7684-4e7d-b321-7da7dc4891ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b110d9-a4d4-47e0-be1e-6faba38fb963");

            migrationBuilder.AlterColumn<int>(
                name: "bagId",
                table: "TokenTransactions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f875bb2c-20a3-4d5d-8c73-4a7b08872dc7", "e0588a26-5735-4527-b50c-a2cddfca4dad", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "865c0fac-881a-462d-a659-43b980b6e768", "ae81a152-ede8-464f-b59a-2bcebc50930f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_TokenTransactions_BagTokens_bagId",
                table: "TokenTransactions",
                column: "bagId",
                principalTable: "BagTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenTransactions_BagTokens_bagId",
                table: "TokenTransactions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "865c0fac-881a-462d-a659-43b980b6e768");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f875bb2c-20a3-4d5d-8c73-4a7b08872dc7");

            migrationBuilder.AlterColumn<int>(
                name: "bagId",
                table: "TokenTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3268f411-7684-4e7d-b321-7da7dc4891ad", "dc09aafe-606e-40ba-8239-241bff54de4f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37b110d9-a4d4-47e0-be1e-6faba38fb963", "c946abb9-b811-4849-bfb0-c98f8f82ee51", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_TokenTransactions_BagTokens_bagId",
                table: "TokenTransactions",
                column: "bagId",
                principalTable: "BagTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
