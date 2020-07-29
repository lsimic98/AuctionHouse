using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionHouse.Migrations
{
    public partial class AuctionInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a19d847-6235-4f34-9cca-bb0f10960584");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "643c59f9-a0aa-423f-9e94-a067746a2cd0");

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    image = table.Column<byte[]>(nullable: false),
                    startPrice = table.Column<int>(nullable: false),
                    currentPrice = table.Column<int>(nullable: false),
                    createDate = table.Column<DateTime>(nullable: false),
                    openDate = table.Column<DateTime>(nullable: false),
                    closeDate = table.Column<DateTime>(nullable: false),
                    state = table.Column<string>(nullable: false),
                    winnerId = table.Column<string>(nullable: true),
                    ownerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auctions_AspNetUsers_ownerId",
                        column: x => x.ownerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auctions_AspNetUsers_winnerId",
                        column: x => x.winnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    userId = table.Column<string>(nullable: false),
                    auctionId = table.Column<int>(nullable: false),
                    price = table.Column<int>(nullable: false),
                    bidDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => new { x.userId, x.auctionId });
                    table.ForeignKey(
                        name: "FK_Bids_Auctions_auctionId",
                        column: x => x.auctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bids_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae846572-350a-4470-af8c-d913b32b7a3d", "f6326f52-b26b-44d9-b6ef-1a6b543825df", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7685171e-58ec-4544-98e0-a5fc4beb27d3", "fa895699-55a1-4aac-9e50-2034b22bcc65", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_ownerId",
                table: "Auctions",
                column: "ownerId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_winnerId",
                table: "Auctions",
                column: "winnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_auctionId",
                table: "Bids",
                column: "auctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7685171e-58ec-4544-98e0-a5fc4beb27d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae846572-350a-4470-af8c-d913b32b7a3d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a19d847-6235-4f34-9cca-bb0f10960584", "f16c6236-c956-4eed-b657-61561a281e60", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "643c59f9-a0aa-423f-9e94-a067746a2cd0", "b5d2b0ae-2662-4b16-aa6a-d872eed33f3f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
