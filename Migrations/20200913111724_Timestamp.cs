using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionHouse.Migrations
{
    public partial class Timestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_AspNetUsers_userId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "865c0fac-881a-462d-a659-43b980b6e768");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f875bb2c-20a3-4d5d-8c73-4a7b08872dc7");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Bids",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Bids",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Auctions",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49c91213-60f8-494a-b94d-82766afd33f3", "80ef91bd-8e7a-414b-b601-0964f6da9e41", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b00ddb4-63e1-4522-863f-c61f157461b1", "e9d7591c-f27a-4614-b4b6-0141ac273210", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_userId",
                table: "Bids",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_AspNetUsers_userId",
                table: "Bids",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_AspNetUsers_userId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_userId",
                table: "Bids");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b00ddb4-63e1-4522-863f-c61f157461b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49c91213-60f8-494a-b94d-82766afd33f3");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Auctions");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Bids",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                columns: new[] { "userId", "auctionId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f875bb2c-20a3-4d5d-8c73-4a7b08872dc7", "e0588a26-5735-4527-b50c-a2cddfca4dad", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "865c0fac-881a-462d-a659-43b980b6e768", "ae81a152-ede8-464f-b59a-2bcebc50930f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_AspNetUsers_userId",
                table: "Bids",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
