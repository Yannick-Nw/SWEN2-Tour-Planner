using System.Collections.Generic;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.BusinessLogic.Services
{
    public interface ITourExporter
    {
        void ExportToursToJson(IEnumerable<Tour> tours, string filePath);
    }
}
