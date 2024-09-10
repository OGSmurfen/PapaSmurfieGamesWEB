using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapaSmurfie.Migrations
{
    /// <inheritdoc />
    public partial class FriendsList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendsList",
                columns: table => new
                {
                    FriendshipRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendshipSenderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendshipReceiverId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendsList", x => x.FriendshipRecordId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendsList");
        }
    }
}
