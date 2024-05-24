using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using TourPlanner.Models;
using System.Windows.Forms; 

//pdfs get saved in the documents folder on your pc

namespace TourPlanner.Report
{
    public static class PdfReportGenerator
    {
        public static void GenerateTourReport(Tour tour)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            try
            {
                // Generate a unique file name using a timestamp
                string fileName = $"TourReport_{tour.Name}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);

                        page.Header()
                            .Text($"Tour Report: {tour.Name}")
                            .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                        page.Content().Column(column =>
                        {
                            column.Spacing(10);

                            column.Item().Text($"Description: {tour.Description}");
                            column.Item().Text($"From: {tour.From}");
                            column.Item().Text($"To: {tour.To}");
                            column.Item().Text($"Transport Type: {tour.TransportType}");
                            column.Item().Text($"Distance: {tour.Distance} km");
                            column.Item().Text($"Estimated Time: {tour.EstimatedTime}");
                            column.Item().Text($"Popularity: {tour.Popularity}");
                            column.Item().Text($"Child Friendliness: {tour.ChildFriendliness}");

                            if (!string.IsNullOrEmpty(tour.TourImage))
                            {
                               
                                column.Item().Image(tour.TourImage);

                            }
                           
                            column.Item().Text("Tour Logs:").FontSize(16).SemiBold().FontColor(Colors.Red.Darken1);

                            foreach (var log in tour.TourLogs)
                            {
                                // Wrap each tour log entry in a Shrink container and apply styling
                                column.Item().Shrink().Padding(5).Background("#f0f0f0")
                                    .Text($"DateTime: {log.DateTime}\n" +
                                    $"Duration: {log.TotalTime}\n" +
                                          $"Distance: {log.TotalDistance} km \n" +
                                          $"Rating: {log.Rating}\n" +
                                          $"Comment: {log.Comment}\n" +
                                          $"Difficulty: {log.Difficulty}");
                            }
                        });

                        page.Footer()
                            .AlignCenter()
                            .Text(x =>
                            {
                                x.Span("Page ");
                                x.CurrentPageNumber();
                            });
                    });
                });

                document.GeneratePdf(filePath);
                // Display a popup message after PDF generation
                ShowPopupMessage("Tour report PDF generated successfully");

                Console.WriteLine($"Tour report PDF generated successfully: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating tour report PDF: {ex.Message}");
            }
        }

        public static void GenerateSummaryReport(IEnumerable<Tour> tours)
        {
            try
            {
                string fileName = $"SummaryReport_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

                QuestPDF.Settings.License = LicenseType.Community;

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);

                        page.Header()
                            .Text("Summary Report")
                            .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                        page.Content().Column(column =>
                        {
                            column.Spacing(10);

                            foreach (var tour in tours)
                            {
                                var averageDuration = tour.TourLogs.Any() ? TimeSpan.FromMinutes(tour.TourLogs.Average(t => t.TotalTime.TotalMinutes)) : TimeSpan.Zero;
                                var averageDistance = tour.TourLogs.Any() ? tour.TourLogs.Average(t => t.TotalDistance) : 0;
                                var averageRating = tour.TourLogs.Any() ? tour.TourLogs.Average(t => t.Rating) : 0;

                                column.Item().Text($"{tour.Name}:").FontSize(16).SemiBold();
                                column.Item().Text($"Average Duration: {averageDuration}");
                                column.Item().Text($"Average Distance: {averageDistance:F2} km");
                                column.Item().Text($"Average Rating: {averageRating:F2}");
                            }
                        });

                        page.Footer()
                            .AlignCenter()
                            .Text(x =>
                            {
                                x.Span("Page ");
                                x.CurrentPageNumber();
                            });
                    });
                });

                document.GeneratePdf(filePath);
                ShowPopupMessage("Summary report PDF generated successfully");

                Console.WriteLine($"Summary report PDF generated successfully: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating summary report PDF: {ex.Message}");
            }
        }

        private static void ShowPopupMessage(string message)
        {
            MessageBox.Show(message, "PDF Generation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}