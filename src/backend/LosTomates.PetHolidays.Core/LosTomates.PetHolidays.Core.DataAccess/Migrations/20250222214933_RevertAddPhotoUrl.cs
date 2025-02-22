using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosTomates.PetHolidays.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RevertAddPhotoUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Hotels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Rooms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Pets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Hotels",
                type: "text",
                nullable: true);
        }
    }
}
