#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashGrounds.Rate.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TrackId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackRates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostRates_PostId",
                table: "PostRates",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostRates_UserId_PostId",
                table: "PostRates",
                columns: new[] { "UserId", "PostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackRates_TrackId",
                table: "TrackRates",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackRates_UserId_TrackId",
                table: "TrackRates",
                columns: new[] { "UserId", "TrackId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostRates");

            migrationBuilder.DropTable(
                name: "TrackRates");
        }
    }
}
