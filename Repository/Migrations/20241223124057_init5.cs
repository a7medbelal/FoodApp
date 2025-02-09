using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Core.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_items",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_items_RecipeID",
                table: "items");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "items");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "items");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "items");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "items");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "items");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "items");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "items");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "items");

            migrationBuilder.AddPrimaryKey(
                name: "PK_items",
                table: "items",
                column: "RecipeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_items",
                table: "items");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_items",
                table: "items",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_items_RecipeID",
                table: "items",
                column: "RecipeID");
        }
    }
}
