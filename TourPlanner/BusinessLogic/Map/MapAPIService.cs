using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Drawing;
using System.IO;

namespace TourPlanner.BusinessLogic.Map
{
    internal class MapAPIService
    {
        public async Task<string> GetGeoCodeAsync(string address, string apiKey)
        {
            string uri = $"https://api.openrouteservice.org/geocode/search?api_key={apiKey}&text={address}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<Bitmap> GetTileAsync(Tile tile, int zoom)
        {
            string uri = $"https://tile.openstreetmap.org/{zoom}/{tile.X}/{tile.Y}.png";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    return new Bitmap(stream);
                }
            }
        }

        public async Task<string> GetDirectionsAsync(string startCoordinates, string endCoordinates, string apiKey)
        {
            string uri = $"https://api.openrouteservice.org/v2/directions/driving-car?api_key={apiKey}&start={startCoordinates}&end={endCoordinates}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
        }


    }
}
