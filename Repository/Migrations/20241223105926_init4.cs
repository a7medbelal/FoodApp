using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Core.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "AspNetUsers",
                newName: "roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "roles",
                table: "AspNetUsers",
                newName: "role");
        }
    }
}
