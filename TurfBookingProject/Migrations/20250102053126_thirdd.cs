using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginFormASPcore.Migrations
{
    /// <inheritdoc />
    public partial class thirdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "EventBookings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EventBookings");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "EventBookings");

            migrationBuilder.RenameColumn(
                name: "Event",
                table: "EventBookings",
                newName: "EventName");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "EventBookings",
                newName: "EventDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "EventBookings",
                newName: "Event");

            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "EventBookings",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "EventBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EventBookings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "EventBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
