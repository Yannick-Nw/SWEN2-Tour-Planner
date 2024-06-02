using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLogic.Map
{
    internal class MapCreator
    {
        private readonly double minLon;
        private readonly double minLat;
        private readonly double maxLon;
        private readonly double maxLat;
        public MapCreator(GeoCoordinate start, GeoCoordinate end)
        {
            this.minLon = start.Lon;
            this.minLat = start.Lat;
            this.maxLon = end.Lon;
            this.maxLat = end.Lat;
        }

        public int Zoom { get; set; } = 18;
        public bool CropImage { get; set; } = true;
        private readonly List<GeoCoordinate> markers = new();
        private Bitmap finalImage;

        public async Task<Bitmap> GenerateImage(MapAPIService api)
        {
            // Calculate the tile numbers for each corner of the bounding box
            var topLeftTile = Tile.LatLonToTile(maxLat, minLon, Zoom);
            var bottomRightTile = Tile.LatLonToTile(minLat, maxLon, Zoom);

            // Determine the number of tiles to fetch in each dimension
            int tilesX = bottomRightTile.X - topLeftTile.X + 1;
            int tilesY = bottomRightTile.Y - topLeftTile.Y + 1;

            // Create a new image to hold all the tiles
            finalImage = new Bitmap(tilesX * 256, tilesY * 256);
            Graphics g = Graphics.FromImage(finalImage);

            // Fetch and draw each tile
            for (int x = topLeftTile.X; x <= bottomRightTile.X; x++)
            {
                for (int y = topLeftTile.Y; y <= bottomRightTile.Y; y++)
                {
                    Bitmap tileImage = await api.GetTileAsync(new Tile(x, y), Zoom);
                    int xPos = (x - topLeftTile.X) * 256;
                    int yPos = (y - topLeftTile.Y) * 256;
                    g.DrawImage(tileImage, xPos, yPos);
                }
            }

            PixelCalculator.Point topLeftTilePixel = new PixelCalculator.Point(topLeftTile.X * 256, topLeftTile.Y * 256);

            // Draw Markers
            foreach (var marker in markers)
            {
                using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Marker.PIN_RED_32px.GetResource().ToString());
                Bitmap markerIcon = new Bitmap(stream);
                PixelCalculator.Point globalPos = PixelCalculator.LatLonToPixel(marker.Lat, marker.Lon, Zoom);
                PixelCalculator.Point relativePos = new PixelCalculator.Point(globalPos.X - topLeftTilePixel.X, globalPos.Y - topLeftTilePixel.Y);
                g.DrawImage(markerIcon, relativePos.X, relativePos.Y);
            }

            // Crop the image to the exact bounding box
            if (CropImage)
            {
                PixelCalculator.Point bboxLeftTopGlobalPos = PixelCalculator.LatLonToPixel(maxLat, minLon, Zoom);
                PixelCalculator.Point bboxRightBottomGlobalPos = PixelCalculator.LatLonToPixel(minLat, maxLon, Zoom);
                PixelCalculator.Point bboxLeftTopRelativePos = new PixelCalculator.Point(bboxLeftTopGlobalPos.X - topLeftTilePixel.X, bboxLeftTopGlobalPos.Y - topLeftTilePixel.Y);
                int width = bboxRightBottomGlobalPos.X - bboxLeftTopGlobalPos.X;
                int height = bboxRightBottomGlobalPos.Y - bboxLeftTopGlobalPos.Y;
                finalImage = finalImage.Clone(new Rectangle(bboxLeftTopRelativePos.X, bboxLeftTopRelativePos.Y, width, height), finalImage.PixelFormat);
            }

            g.Dispose();
        }








        public Bitmap StitchTilesTogether(List<Bitmap> tiles)
        {
            int tileWidth = tiles[0].Width;
            int tileHeight = tiles[0].Height;
            int outputWidth = tileWidth * tiles.Count;
            int outputHeight = tileHeight;

            Bitmap output = new Bitmap(outputWidth, outputHeight);
            using (Graphics g = Graphics.FromImage(output))
            {
                for (int i = 0; i < tiles.Count; i++)
                {
                    g.DrawImage(tiles[i], i * tileWidth, 0);
                }
            }

            return output;
        }
    }
}
