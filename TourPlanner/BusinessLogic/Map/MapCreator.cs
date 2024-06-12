﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

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
            this.minLon = Math.Min(start.Lon, end.Lon);
            this.minLat = Math.Min(start.Lat, end.Lat);
            this.maxLon = Math.Max(start.Lon, end.Lon);
            this.maxLat = Math.Max(start.Lat, end.Lat);
        }

        public int Zoom { get; set; } = 18;
        public bool CropImage { get; set; } = true;

        private readonly List<GeoCoordinate> markers = new();
        private readonly List<GeoCoordinate> routeWaypoints = new();
        private Bitmap finalImage;

        public void AddMarker(GeoCoordinate marker)
        {
            markers.Add(marker);
        }

        public void AddRouteWaypoints(List<GeoCoordinate> waypoints)
        {
            routeWaypoints.AddRange(waypoints);
        }

        public async Task<Bitmap> GenerateImage(MapAPIService api)
        {
            // Calculate the tile numbers for each corner of the bounding box
            var topLeftTile = Tile.LatLonToTile(maxLat, minLon, Zoom);
            var bottomRightTile = Tile.LatLonToTile(minLat, maxLon, Zoom);

            // Determine the number of tiles to fetch in each dimension
            int tilesX = bottomRightTile.X - topLeftTile.X + 1;
            int tilesY = bottomRightTile.Y - topLeftTile.Y + 1;

            // Ensure tilesX and tilesY are valid
            if (tilesX <= 0 || tilesY <= 0)
            {
                throw new ArgumentException("Invalid dimensions for the bitmap.");
            }

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

            Point topLeftTilePixel = new Point(topLeftTile.X * 256, topLeftTile.Y * 256);

            // Draw route waypoints as lines
            if (routeWaypoints.Count > 1)
            {
                Pen pen = new Pen(Color.Red, 10);
                for (int i = 0; i < routeWaypoints.Count - 1; i++)
                {
                    Point point1 = Point.LatLonToPixel(routeWaypoints[i].Lat, routeWaypoints[i].Lon, Zoom);
                    Point point2 = Point.LatLonToPixel(routeWaypoints[i + 1].Lat, routeWaypoints[i + 1].Lon, Zoom);

                    Point relativePos1 = new Point(point1.X - topLeftTilePixel.X, point1.Y - topLeftTilePixel.Y);
                    Point relativePos2 = new Point(point2.X - topLeftTilePixel.X, point2.Y - topLeftTilePixel.Y);

                    // Ensure points are within image bounds before drawing
                    if (IsWithinBounds(relativePos1, finalImage.Width, finalImage.Height) &&
                        IsWithinBounds(relativePos2, finalImage.Width, finalImage.Height))
                    {
                        g.DrawLine(pen, relativePos1.X, relativePos1.Y, relativePos2.X, relativePos2.Y);
                    }
                }

                pen.Dispose();
            }

            // Draw Markers
            foreach (var marker in markers)
            {
                //using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Marker.PIN_RED_32px.GetResource().ToString());
                //Bitmap markerIcon = new Bitmap(stream);

                Bitmap markerIcon = MarkerUtils.GetMarkerImage(Marker.PIN_RED_32px);
                Point globalPos = Point.LatLonToPixel(marker.Lat, marker.Lon, Zoom);
                Point relativePos = new Point(globalPos.X - topLeftTilePixel.X, globalPos.Y - topLeftTilePixel.Y);

                // Ensure marker is within image bounds before drawing
                if (IsWithinBounds(relativePos, finalImage.Width, finalImage.Height))
                {
                    g.DrawImage(markerIcon, relativePos.X, relativePos.Y);
                }
            }

            // Crop the image to the exact bounding box
            if (CropImage)
            {
                Point bboxLeftTopGlobalPos = Point.LatLonToPixel(maxLat, minLon, Zoom);
                Point bboxRightBottomGlobalPos = Point.LatLonToPixel(minLat, maxLon, Zoom);
                Point bboxLeftTopRelativePos = new Point(bboxLeftTopGlobalPos.X - topLeftTilePixel.X,
                    bboxLeftTopGlobalPos.Y - topLeftTilePixel.Y);
                int width = bboxRightBottomGlobalPos.X - bboxLeftTopGlobalPos.X;
                int height = bboxRightBottomGlobalPos.Y - bboxLeftTopGlobalPos.Y;

                // Debugging output for cropping dimensions
                Console.WriteLine(
                    $"bboxLeftTopRelativePos: X={bboxLeftTopRelativePos.X}, Y={bboxLeftTopRelativePos.Y}");
                Console.WriteLine($"Width: {width}, Height: {height}");

                // Ensure width and height are valid
                if (width > 0 && height > 0)
                {
                    finalImage =
                        finalImage.Clone(
                            new Rectangle(bboxLeftTopRelativePos.X, bboxLeftTopRelativePos.Y, width, height),
                            finalImage.PixelFormat);
                }
                else
                {
                    throw new ArgumentException("Invalid dimensions for the cropped image.");
                }
            }

            g.Dispose();
            return finalImage;
        }

        public async Task AddRouteAsync(MapAPIService api, GeoCoordinate start, GeoCoordinate end)
        {
            // Ensure the correct decimal separator
            string startCoordinates =
                $"{start.Lon.ToString(System.Globalization.CultureInfo.InvariantCulture)},{start.Lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
            string endCoordinates =
                $"{end.Lon.ToString(System.Globalization.CultureInfo.InvariantCulture)},{end.Lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}";

            string directionsJson = await api.GetDirectionsAsync(startCoordinates, endCoordinates);

            // Log the JSON response
            //Console.WriteLine(directionsJson);

            var waypoints = ParseWaypoints(directionsJson);

            AddRouteWaypoints(waypoints);
        }

        private List<GeoCoordinate> ParseWaypoints(string directionsJson)
        {
            var waypoints = new List<GeoCoordinate>();
            using (JsonDocument doc = JsonDocument.Parse(directionsJson))
            {
                var root = doc.RootElement;

                if (root.TryGetProperty("features", out JsonElement features))
                {
                    if (features.GetArrayLength() > 0 &&
                        features[0].TryGetProperty("geometry", out JsonElement geometry))
                    {
                        if (geometry.TryGetProperty("coordinates", out JsonElement coordinates))
                        {
                            foreach (var coordinate in coordinates.EnumerateArray())
                            {
                                var lon = coordinate[0].GetDouble();
                                var lat = coordinate[1].GetDouble();
                                waypoints.Add(new GeoCoordinate(lon, lat));
                            }
                        }
                        else
                        {
                            Console.WriteLine("No 'coordinates' property found in 'geometry'.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No 'geometry' property found in the first feature.");
                    }
                }
                else
                {
                    Console.WriteLine("No 'features' property found in the root element.");
                }
            }

            return waypoints;
        }

        private bool IsWithinBounds(Point point, int width, int height)
        {
            return point.X >= 0 && point.X < width && point.Y >= 0 && point.Y < height;
        }
    }
}