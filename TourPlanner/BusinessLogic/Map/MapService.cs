using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace TourPlanner.BusinessLogic.Map
{
    internal class MapService
    {
        private Bitmap finalImage;

        public async Task GetMap(string apiKey, string adressStart, string adressEnd)
        {
            var request = new MapAPIService(apiKey);
            GeoCoordinate locationDataStart = await request.GetGeoCodeAsync(adressStart);
            GeoCoordinate locationDataEnd = await request.GetGeoCodeAsync(adressEnd);
            MapCreator mapCreator = new MapCreator(locationDataStart, locationDataEnd);
            mapCreator.Zoom = 18;
            mapCreator.AddMarker(locationDataStart);
            mapCreator.AddMarker(locationDataEnd);

            finalimage = mapCreator.GenerateImage(request);
            SaveImage("FHTW-map.png");
        }

        public void SaveImage(string filename)
        {
            finalImage.Save(filename, ImageFormat.Png);
        }

    }
}
