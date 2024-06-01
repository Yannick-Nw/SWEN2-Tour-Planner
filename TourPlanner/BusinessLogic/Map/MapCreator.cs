using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLogic.Map
{
    internal class MapCreator
    {
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
