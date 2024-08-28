using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapaSmurfie.Migrations
{
    /// <inheritdoc />
    public partial class OwnedGameModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnedGames_AspNetUsers_UserOwnerId",
                table: "OwnedGames");

            migrationBuilder.DropForeignKey(
                name: "FK_OwnedGames_Games_GameOwnedId",
                table: "OwnedGames");

            migrationBuilder.DropIndex(
                name: "IX_OwnedGames_GameOwnedId",
                table: "OwnedGames");

            migrationBuilder.DropIndex(
                name: "IX_OwnedGames_UserOwnerId",
                table: "OwnedGames");

            migrationBuilder.DropColumn(
                name: "UserOwnerId",
                table: "OwnedGames");

            migrationBuilder.AddColumn<string>(
                name: "UserOwnerName",
                table: "OwnedGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserOwnerName",
                table: "OwnedGames");

            migrationBuilder.AddColumn<string>(
                name: "UserOwnerId",
                table: "OwnedGames",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedGames_GameOwnedId",
                table: "OwnedGames",
                column: "GameOwnedId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedGames_UserOwnerId",
                table: "OwnedGames",
                column: "UserOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedGames_AspNetUsers_UserOwnerId",
                table: "OwnedGames",
                column: "UserOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnedGames_Games_GameOwnedId",
                table: "OwnedGames",
                column: "GameOwnedId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
