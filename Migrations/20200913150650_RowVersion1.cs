using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionHouse.Migrations
{
    public partial class RowVersion1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b00ddb4-63e1-4522-863f-c61f157461b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49c91213-60f8-494a-b94d-82766afd33f3");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Auctions");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Auctions",
                rowVersion: true,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "734f0ae5-93b6-43c4-a0de-b398061029cd", "50d8819b-1ba4-4726-b5b4-31a7eb160b57", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5a102602-cab1-4ec0-865c-465b97c24aea", "ac8050a8-558d-474a-a580-98ac1a4fd595", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a102602-cab1-4ec0-865c-465b97c24aea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "734f0ae5-93b6-43c4-a0de-b398061029cd");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Auctions");

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Auctions",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49c91213-60f8-494a-b94d-82766afd33f3", "80ef91bd-8e7a-414b-b601-0964f6da9e41", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b00ddb4-63e1-4522-863f-c61f157461b1", "e9d7591c-f27a-4614-b4b6-0141ac273210", "Administrator", "ADMINISTRATOR" });
        }
    }
}
