using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Again_AuthDatabase_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2e78b7a8dafd45a6bc18e8204ed67958",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76a7e019-dd08-4cbe-8e09-215858d3840f", "AQAAAAIAAYagAAAAEKs9ZOJgvPAar9wfLGN9+KPdUtQ9zWxT/GqQjXcBJXX3aog8YmpTAT6vKP7HTqrJJQ==", "00f56960-8a42-40b0-b9d0-987c1779424b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2e78b7a8dafd45a6bc18e8204ed67958",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9439073-f789-4426-bf86-5daf207e3559", "Admin@123", "2d52c623-7a73-4ac1-9de0-45b4442d5173" });
        }
    }
}
