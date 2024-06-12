﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TourPlanner.BusinessLogic.Map
{
    public enum Marker
    {
        PIN_RED_16px,
        PIN_RED_32px,
        MARKER_RED_16px,
        MARKER_RED_32px
    }

    public static class MarkerUtils
    {
        private static readonly Dictionary<Marker, string> MarkerFilenames = new()
        {
            { Marker.PIN_RED_16px, "pin-red_16px" },
            { Marker.PIN_RED_32px, "pin-red_32px" },
            { Marker.MARKER_RED_16px, "marker-red_16px" },
            { Marker.MARKER_RED_32px, "marker-red_32px" }
        };

        public static string GetResource(Marker marker)
        {
            string filename = MarkerFilenames[marker] + ".png";
            string baseDirectory = AppContext.BaseDirectory;
            string resourcePath = FindDirectoryWithResources(baseDirectory);

            if (resourcePath == null)
            {
                throw new DirectoryNotFoundException("Resources directory not found.");
            }

            resourcePath = Path.Combine(resourcePath, "Map", "Resources", filename);
            Console.WriteLine($"Resource path: {resourcePath}"); // Log the resource path
            return resourcePath;
        }

        private static string FindDirectoryWithResources(string startDirectory)
        {
            string currentDirectory = startDirectory;
            while (!string.IsNullOrEmpty(currentDirectory))
            {
                string potentialPath = Path.Combine(currentDirectory, "Map", "Resources");
                if (Directory.Exists(potentialPath))
                {
                    return currentDirectory;
                }
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            }
            return null;
        }

        public static Bitmap GetMarkerImage(Marker marker)
        {
            string resourcePath = GetResource(marker);
            if (!File.Exists(resourcePath))
            {
                throw new FileNotFoundException($"The file '{resourcePath}' does not exist.");
            }

            try
            {
                return new Bitmap(resourcePath);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Failed to load the image from '{resourcePath}'. Ensure the file is a valid PNG image.", ex);
            }
        }
    }
}