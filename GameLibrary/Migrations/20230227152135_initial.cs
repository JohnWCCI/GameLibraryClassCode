using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameLibrary.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishersId = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardGames_Publishers_PublishersId",
                        column: x => x.PublishersId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Days of Wonder" },
                    { 2, "Stonemaier Games" },
                    { 3, "Hasbro" },
                    { 4, "Spin Master" }
                });

            migrationBuilder.InsertData(
                table: "BoardGames",
                columns: new[] { "Id", "Description", "ImageURL", "Name", "PublishersId" },
                values: new object[,]
                {
                    { 1, "Control one fantasy race after another to expand throught the land", "https://cf.geekdo-images.com/aoPM07XzoceB-RydLh08zA__imagepage/img/lHmv0ddOrUvpiLcPeQbZdT5yCEA=/fit-in/900x600/filters:no_upscale():strip_icc()/pic428828.jpg", "SmallWorld", 1 },
                    { 2, "Attract a beautiful and diversecollection of birds to your wildlife preserve.", "https://cf.geekdo-images.com/yLZJCVLlIx4c7eJEWUNJ7w__imagepagezoom/img/yS4vL6iTCvHSvGySxyOjV_-R3dI=/fit-in/1200x900/filters:no_upscale():strip_icc()/pic4458123.jpg", "WingSpan", 2 },
                    { 3, "Hasbro Gaming Trouble Board Game for Kids Ages 5 and Up 2-4 Players", "https://m.media-amazon.com/images/I/81MdgnO4l9L._AC_UL400_.jpg", "Trouble", 3 },
                    { 4, "Hasbro Gaming The Game of Life Board Game Ages 8 & Up", "https://m.media-amazon.com/images/I/81yQxkx3vwL._AC_UL640_QL65_.jpg", "The Game of Life", 3 },
                    { 5, "Hasbro Gaming Candy Land Kingdom Of Sweet Adventures Board Game For Kids Ages", "https://m.media-amazon.com/images/I/91yUG40gv0L._AC_UL400_.jpg", "Candy Land", 3 },
                    { 6, "Hasbro Gaming Risk Military Wargame", "https://m.media-amazon.com/images/I/91jsvpbPP3L._AC_UL400_.jpg", "Risk", 3 },
                    { 7, "Ticket to Ride Board Game | Family Board Game | Board Game for Adults and Family", "https://m.media-amazon.com/images/I/91YNJM4oyhL._AC_UL400_.jpg", "Ticket to ride", 1 },
                    { 8, "SORRY Classic Family Board Game Indoor Outdoor Retro Party Activity Summer Toy with Oversized Gameboard", "https://m.media-amazon.com/images/I/81ItkRyOaaL._AC_UL400_.jpg", "Sorry", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGames_PublishersId",
                table: "BoardGames",
                column: "PublishersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
