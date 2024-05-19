using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using log4net;


#nullable disable

namespace TourPlanner.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(InitialCreate));

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            try
            {
                log.Info("Migration Up method started.");

                migrationBuilder.CreateTable(
                    name: "Tours",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        Name = table.Column<string>(type: "text", nullable: false),
                        Description = table.Column<string>(type: "text", nullable: false),
                        From = table.Column<string>(type: "text", nullable: false),
                        To = table.Column<string>(type: "text", nullable: false),
                        TransportType = table.Column<string>(type: "text", nullable: false),
                        Distance = table.Column<double>(type: "double precision", nullable: false),
                        EstimatedTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                        TourImage = table.Column<string>(type: "text", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Tours", x => x.Id);
                    });

                migrationBuilder.CreateTable(
                    name: "TourLogs",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "integer", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                        Comment = table.Column<string>(type: "text", nullable: true),
                        Difficulty = table.Column<int>(type: "integer", nullable: false),
                        TotalDistance = table.Column<double>(type: "double precision", nullable: false),
                        TotalTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                        Rating = table.Column<int>(type: "integer", nullable: false),
                        TourId = table.Column<int>(type: "integer", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_TourLogs", x => x.Id);
                        table.ForeignKey(
                            name: "FK_TourLogs_Tours_TourId",
                            column: x => x.TourId,
                            principalTable: "Tours",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_TourLogs_TourId",
                    table: "TourLogs",
                    column: "TourId");

                log.Info("Migration Up method completed successfully.");
            }
            catch (Exception ex)
            {
                log.Error("An error occurred during the migration Up method.", ex);
                throw;
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            try
            {
                log.Info("Migration Down method started.");

                migrationBuilder.DropTable(name: "TourLogs");
                migrationBuilder.DropTable(name: "Tours");

                log.Info("Migration Down method completed successfully.");
            }
            catch (Exception ex)
            {
                log.Error("An error occurred during the migration Down method.", ex);
                throw;
            }
        }
    }
}