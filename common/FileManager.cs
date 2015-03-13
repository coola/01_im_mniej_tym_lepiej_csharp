using System.Collections.Generic;
using System.IO;
using System.Text;

namespace common
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

        public static string ConvertPointsIntoData(List<GPS_Point> points)
        {
            var result = new StringBuilder();

            foreach (var point in points)
            {
                result.Append(point.Latitude + ',' + point.Longitude);
            }
            return result.ToString();

        }
    }
}
