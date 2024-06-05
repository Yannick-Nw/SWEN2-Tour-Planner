using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.BusinessLogic.Services
{
    public class TourExporter : ITourExporter
    {
        public void ExportToursToJson(IEnumerable<Tour> tours, string filePath)
        {
            // Serialize the list of tours to JSON format
            string jsonData = JsonConvert.SerializeObject(tours, Formatting.Indented);

            // Write the JSON data to the specified file
            File.WriteAllText(filePath, jsonData);
        }
    }
}
