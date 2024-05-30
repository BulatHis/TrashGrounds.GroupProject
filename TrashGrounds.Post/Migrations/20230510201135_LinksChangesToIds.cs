using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrashGrounds.Post.Migrations
{
    /// <inheritdoc />
    public partial class LinksChangesToIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetLink",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "Posts",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "AssetLink",
                table: "Posts",
                type: "text",
                nullable: true);
        }
    }
}
