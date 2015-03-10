using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace compress
{
    public class FileManager
    {

        public dynamic loadFile(string filename)
        {

            return File.ReadAllLines(filename);

        }

        public static List<GPS_Point> convertDataIntoPoints(string[] data)
        {
            var result = new List<GPS_Point>{};

            foreach (string line in data)
	            {
                    var lineParts = line.Split(',');
		            result.Add(new GPS_Point{Latitude = lineParts[0], Longitude = lineParts[1]});
	            }
            return result;
        
        }
    }
}
