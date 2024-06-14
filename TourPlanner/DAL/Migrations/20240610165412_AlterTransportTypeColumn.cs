using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlanner.Migrations
{
    /// <inheritdoc />
    public partial class AlterTransportTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Tours\" ALTER COLUMN \"TransportType\" TYPE integer USING \"TransportType\"::integer;");

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Tours",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Tours");

            migrationBuilder.Sql("ALTER TABLE \"Tours\" ALTER COLUMN \"TransportType\" TYPE text USING \"TransportType\"::text;");

        }
    }
}
