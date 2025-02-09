using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Core.Migrations
{
    /// <inheritdoc />
    public partial class @int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fname", "Lname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "roles" },
                values: new object[] { "Ahmedbela*-l117200@4@872004", 0, "d8e98f63-0e66-48e0-bb3b-4fcf63d86c72", "legendahmed.122@gmail.com", true, "Ahmed", "belal", true, null, "LEGENDAHMED.122@GMAIL.COM", "AHMED@7", "AQAAAAIAAYagAAAAEHKjRUej1GUUadc6DnpOIQrl+tf0cv3ITg3hWU71FvQR+Gfdx1y3o/WQVg1Rbg00AQ==", null, false, "a528850f-876b-4e0d-b74f-e4caf1d3121e", false, "ahmed@7", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "Ahmedbela*-l117200@4@872004");
        }
    }
}
