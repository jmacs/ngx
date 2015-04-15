using System;
using System.IO;
using System.Xml;
using NgxLib;
using NgxLib.Maps.Serialization;

namespace MapCompiler
{
    public class TiledProcessor
    {
        public MapData Process(string path)
        {
            Console.WriteLine("Processing {0}", path);

            var doc = new XmlDocument();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                doc.Load(stream);
            }

            return Process(doc);
        }

        public MapData Process(XmlDocument doc)
        {
            var map = new MapData();
            map.Width = doc.SelectInt("/map/@width");
            map.Height = doc.SelectInt("/map/@height");
            map.BackgroundColor = doc.Select("/map/@backgroundcolor");
            map.Tileset = doc.Select("/map/tileset/@name");

            foreach (XmlElement prop in doc.SelectNodes("/map/properties/property"))
            {
                if (prop.GetAttribute("name") == "ID")
                {
                    var mapid = prop.GetAttribute("value");

                    if (mapid.Length < 4)
                    {
                        throw new Exception("Map ID must be 4 numbers");
                    }
                    map.MID = int.Parse(mapid);
                }
            }

            var t = doc.SelectSingleNode("/map/layer[@name='Terrain']");
            if (t == null)
            {
                throw new Exception("Missing terrain tile layer!");
            }

            var terrain = doc.SelectNodes("/map/layer[@name='Terrain']/data/tile").GetEnumerator();

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    terrain.MoveNext();

                    var element = terrain.Current as XmlElement;

                    var id = element.GetAttributeInt("gid");
                    if (id == 0) continue;

                    var cell = new CellData();
                    cell.Id = id;
                    cell.X = x;
                    cell.Y = y;
                    map.Terrain.Add(cell);
                }
            }

            var objectGroup = doc.SelectNodes("/map/objectgroup");
            if (objectGroup == null || objectGroup.Count == 0)
            {
                Console.WriteLine("No /map/objectgroup node!");
                return map;
            }

            //var tileheight = int.Parse(doc.Select("/map/@tileheight"));

            foreach (XmlElement obj in doc.SelectNodes("/map/objectgroup/object"))
            {
                var name = obj.GetAttribute("name");
                if (string.IsNullOrWhiteSpace(name))
                {
                    var id = obj.GetAttributeInt("gid");
                    Console.WriteLine("skipping object {0} - no name", id);
                    continue;
                }

                var type = obj.GetAttribute("type");                
                int uid;
                int.TryParse(type, out uid);

                var x = obj.GetAttribute("x");
                var y = obj.GetAttribute("y");

                var mapobj = new MapObjectData();
                mapobj.Prefab = name;
                mapobj.X = int.Parse(x);
                mapobj.Y = int.Parse(y);
                mapobj.UID = uid;
                mapobj.MID = map.MID;
                map.Objects.Add(mapobj);

                var properties = obj.SelectNodes("properties/property");

                foreach (XmlElement property in properties)
                {
                    var n = property.GetAttribute("name");
                    var v = property.GetAttribute("value");
                    if (!mapobj.Args.ContainsKey(n))
                    {
                        mapobj.Args.Add(n, v);
                    }
                }
            }


            var m = doc.SelectSingleNode("/map/layer[@name='BackMask']");
            if (m == null)
            {
                Console.WriteLine("No back mask tile layer");
            }

            if (m != null)
            {
                var bmask = doc.SelectNodes("/map/layer[@name='BackMask']/data/tile").GetEnumerator();

                for (int y = 0; y < map.Height; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        bmask.MoveNext();

                        var element = bmask.Current as XmlElement;

                        var id = element.GetAttributeInt("gid");
                        if (id == 0) continue;

                        var cell = new CellData();
                        cell.Id = id;
                        cell.X = x;
                        cell.Y = y;
                        map.BackMask.Add(cell);
                    }
                }
            }

            return map;
        }

    }
}
