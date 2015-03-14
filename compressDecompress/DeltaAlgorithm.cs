using System.Collections.Generic;
using System.Text;
using common;

namespace compressDecompress
{
    public class DeltaAlgorithm : Algorithm
    {
        public DeltaAlgorithm(PointAlgorithm decompressorAlgorithm)
            : base(decompressorAlgorithm)
        {
        }

        public override string Compress(List<GPS_Point> points)
        {
            var compressedResult = new StringBuilder();

            for (var index = 0; index < points.Count; index++)
            {
                var point = points[index];

                if (index == 0)
                {
                    compressedResult.Append(InnerAlgorithm.CompressPoint(point));
                }
                else
                {

                    var previousPoint = points[index - 1];

                    compressedResult.AppendFormat( "|{0}|{1}", (point.GetLatitudeRightSide - previousPoint.GetLatitudeRightSide ), (point.GetLongitudeRightSide - previousPoint.GetLongitudeRightSide));
                }
            }

            return compressedResult.ToString();
        }

        public override List<GPS_Point> Decompress(string compressedString)
        {

            var resultList = new List<GPS_Point>();

            var desplittedArray = compressedString.Split('|');

            var pointLatitudeLeftSide = desplittedArray[0];
            var pointLatitudeRightSide = desplittedArray[1];

            var pointLongitudeLeftSide = desplittedArray[2];
            var pointLongitudeRightSide = desplittedArray[3];

            var currentPoint = CreateGPSPoint(pointLatitudeLeftSide, pointLatitudeRightSide, pointLongitudeLeftSide, pointLongitudeRightSide);

            resultList.Add(currentPoint);

            for (var i = 4; i < desplittedArray.Length; i=i+2)
            {
                var currentLatitudeRightSideChange = int.Parse(desplittedArray[i]);
                var currentLongitudeRightSideChange = int.Parse(desplittedArray[i+1]);

                pointLatitudeRightSide = (int.Parse(pointLatitudeRightSide) + currentLatitudeRightSideChange).ToString();

                pointLongitudeRightSide = (int.Parse(pointLongitudeRightSide) + currentLongitudeRightSideChange).ToString();

                var nextPoint = CreateGPSPoint(pointLatitudeLeftSide, pointLatitudeRightSide, pointLongitudeLeftSide, pointLongitudeRightSide);

                resultList.Add(nextPoint);

            }

            return resultList;
        }

        private static GPS_Point CreateGPSPoint(string latitudeLeftSide, string latitudeRightSide, string longitudeLeftSide, string longitudeRightSide)
        {
             return new GPS_Point {
                 Latitude = string.Format("{0}.{1}", latitudeLeftSide, Strings.PadWithZeroes(latitudeRightSide)),
                 Longitude = string.Format("{0}.{1}", longitudeLeftSide, Strings.PadWithZeroes(longitudeRightSide))
            };
        }
    }
}