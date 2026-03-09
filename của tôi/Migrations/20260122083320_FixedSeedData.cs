using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_BongDa.Migrations
{
    /// <inheritdoc />
    public partial class FixedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Played = table.Column<int>(type: "int", nullable: false),
                    Won = table.Column<int>(type: "int", nullable: false),
                    Drawn = table.Column<int>(type: "int", nullable: false),
                    Lost = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    YellowCards = table.Column<int>(type: "int", nullable: false),
                    RedCards = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Drawn", "Logo", "Lost", "Name", "Played", "Points", "RedCards", "Won", "YellowCards" },
                values: new object[,]
                {
                    { 1, 1, "https://ssl.gstatic.com/onebox/media/sports/logos/z4rcs1mYv6ADCPrSubNzSw_96x96.png", 1, "Manchester City", 10, 25, 0, 8, 5 },
                    { 2, 2, "https://ssl.gstatic.com/onebox/media/sports/logos/4u_0u9S50Xf33_Pr66pGsw_96x96.png", 1, "Arsenal", 10, 23, 1, 7, 12 },
                    { 3, 3, "https://ssl.gstatic.com/onebox/media/sports/logos/09_NAnE76Tsk0SByEwM0Bw_96x96.png", 1, "Liverpool", 10, 21, 0, 6, 8 },
                    { 4, 3, "https://ssl.gstatic.com/onebox/media/sports/logos/udn4nS4498v_R9L5A1_V3g_96x96.png", 3, "Manchester United", 10, 15, 2, 4, 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
