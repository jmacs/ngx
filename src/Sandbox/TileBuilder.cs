using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TilesetBuilder
{
    public class TileBuilder
    {
        public void Execute(int imageWidth, int imageHeight, int tileWidth, int tileHeight, int spacing)
        {
            var tiles = new List<Tile>();

            var rows = imageWidth / (tileWidth + spacing);
            var cols = imageHeight / (tileHeight + spacing);

            for (int c = 0; c < cols; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    var tile = new Tile();
                    tile.X = (r * tileWidth) + (r * spacing) + 1;
                    tile.Y = (c * tileHeight) + (c * spacing) + 1;
                    tile.Id = c* cols + r + 1;
                    tile.Height = tileHeight;
                    tile.Width = tileWidth;
                    tiles.Add(tile);
                }
            }

            using (var stream = new FileStream("./tiles.xml", FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(tiles.GetType());
                serializer.Serialize(stream, tiles);
            }
            
        }
    }
}