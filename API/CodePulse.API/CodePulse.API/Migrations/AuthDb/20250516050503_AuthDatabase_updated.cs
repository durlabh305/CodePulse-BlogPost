using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AuthDatabase_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2e78b7a8dafd45a6bc18e8204ed67958",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9439073-f789-4426-bf86-5daf207e3559", "Admin@123", "2d52c623-7a73-4ac1-9de0-45b4442d5173" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2e78b7a8dafd45a6bc18e8204ed67958",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "315f1ba0-f5d6-42aa-ba5a-193c9acbdf5f", "AQAAAAEAACcQAAAAEJ1I89Uq...", "25a882fa-642d-4d3d-b1d9-85a49bb2ab19" });
        }
    }
}
