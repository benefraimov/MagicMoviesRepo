using Microsoft.EntityFrameworkCore.Migrations;

namespace MagicMoviesBackend.Migrations
{
    public partial class NewImplementationOfSubscriberMovieAndMovieSubscriberClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscribers_Movies_MovieId",
                table: "Subscribers");

            migrationBuilder.DropIndex(
                name: "IX_Subscribers_MovieId",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Subscribers");

            migrationBuilder.CreateTable(
                name: "MovieSubscribers",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    SubscriberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieSubscribers", x => new { x.MovieId, x.SubscriberId });
                    table.ForeignKey(
                        name: "FK_MovieSubscribers_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieSubscribers_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "SubscriberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieSubscribers_SubscriberId",
                table: "MovieSubscribers",
                column: "SubscriberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieSubscribers");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Subscribers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_MovieId",
                table: "Subscribers",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribers_Movies_MovieId",
                table: "Subscribers",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
