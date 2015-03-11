using System.Collections.Generic;
using System.IO;

namespace compress
{
    public class FileManager
    {

        public dynamic LoadFile(string filename)
        {

            return File.ReadAllLines(filename);

        }

        public static List<GPS_Point> ConvertDataIntoPoints(string[] data)
        {
            var result = new List<GPS_Point>();

            foreach (var line in data)
	            {
                    var lineParts = line.Split(',');
		            result.Add(new GPS_Point{Latitude = lineParts[0], Longitude = lineParts[1]});
	            }
            return result;
        
        }
    }
}
