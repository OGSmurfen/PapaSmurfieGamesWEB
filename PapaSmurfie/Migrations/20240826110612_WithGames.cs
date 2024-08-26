using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapaSmurfie.Migrations
{
    /// <inheritdoc />
    public partial class WithGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameRating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
