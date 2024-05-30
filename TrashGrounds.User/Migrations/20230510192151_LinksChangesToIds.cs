using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrashGrounds.User.Migrations
{
    /// <inheritdoc />
    public partial class LinksChangesToIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7acbbce7-ec2a-4898-945a-c0d203561339"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a462b26d-9e45-40af-b6a6-f5677058f42b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a9a6cc61-c9e3-4590-bc8f-cea22d60b438"));

            migrationBuilder.DropColumn(
                name: "AvatarLink",
                table: "DomainUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "DomainUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("59174f05-e8b2-444c-8e4e-bfc13e25aba6"), null, "Admin", "ADMIN" },
                    { new Guid("6fe9e721-4910-4b2c-b626-23949805fa61"), null, "User", "USER" },
                    { new Guid("c5d94634-95d8-424c-98b1-214dbeafbade"), null, "Moderator", "MODERATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("59174f05-e8b2-444c-8e4e-bfc13e25aba6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6fe9e721-4910-4b2c-b626-23949805fa61"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c5d94634-95d8-424c-98b1-214dbeafbade"));

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "DomainUsers");

            migrationBuilder.AddColumn<string>(
                name: "AvatarLink",
                table: "DomainUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7acbbce7-ec2a-4898-945a-c0d203561339"), null, "Moderator", "MODERATOR" },
                    { new Guid("a462b26d-9e45-40af-b6a6-f5677058f42b"), null, "User", "USER" },
                    { new Guid("a9a6cc61-c9e3-4590-bc8f-cea22d60b438"), null, "Admin", "ADMIN" }
                });
        }
    }
}
