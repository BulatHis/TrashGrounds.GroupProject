using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrashGrounds.Track.Migrations
{
    /// <inheritdoc />
    public partial class LinksChangesToIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("262e33dc-763e-4237-b8cb-c612986ff892"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("40f2239e-f12a-40d4-95b2-f2c650b1d983"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d9683c50-cb9a-4b8f-b79b-1655f79b1c42"));

            migrationBuilder.DropColumn(
                name: "MusicLink",
                table: "MusicTracks");

            migrationBuilder.DropColumn(
                name: "PictureLink",
                table: "MusicTracks");

            migrationBuilder.AddColumn<Guid>(
                name: "MusicId",
                table: "MusicTracks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PictureId",
                table: "MusicTracks",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("251ceebe-752a-4ea4-810e-03b8eaee7e5f"), "Что-то странное" },
                    { new Guid("50e3aa21-ada0-42d3-a143-cf45922b97a5"), "Мэшап" },
                    { new Guid("d4b7bc34-3b01-4247-a9bc-02d4c5185e08"), "Русский рэп" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("251ceebe-752a-4ea4-810e-03b8eaee7e5f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("50e3aa21-ada0-42d3-a143-cf45922b97a5"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d4b7bc34-3b01-4247-a9bc-02d4c5185e08"));

            migrationBuilder.DropColumn(
                name: "MusicId",
                table: "MusicTracks");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "MusicTracks");

            migrationBuilder.AddColumn<string>(
                name: "MusicLink",
                table: "MusicTracks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureLink",
                table: "MusicTracks",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("262e33dc-763e-4237-b8cb-c612986ff892"), "Русский рэп" },
                    { new Guid("40f2239e-f12a-40d4-95b2-f2c650b1d983"), "Мэшап" },
                    { new Guid("d9683c50-cb9a-4b8f-b79b-1655f79b1c42"), "Что-то странное" }
                });
        }
    }
}
