using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using log4net;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.DAL
{
    public class TourPlannerContext : DbContext
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TourPlannerContext));

        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLog> TourLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                // Retrieve connection string from app.config
                string connectionString = ConfigurationManager.ConnectionStrings["TourPlannerConnection"].ConnectionString;

                optionsBuilder.UseNpgsql(connectionString);
                log.Info("Database configuration set to use Npgsql.");
            }
            catch (Exception ex)
            {
                log.Error("An error occurred while configuring the database.", ex);
                throw;
            }
        }
    }
}
