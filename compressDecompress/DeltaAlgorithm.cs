using System.Collections.Generic;
using System.Text;
using common;

namespace compressDecompress
{
    public class DeltaAlgorithm : Algorithm
    {
        public DeltaAlgorithm(PointAlgorithm pointAlgorithm)
            : base(pointAlgorithm)
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

                    if (previousPoint.GetLatitudeLeftSide != point.GetLatitudeLeftSide)
                    {
                        compressedResult.AppendFormat("|la{0}",
                            point.GetLatitudeLeftSide - previousPoint.GetLatitudeLeftSide);
                    }

                    if (previousPoint.GetLongitudeLeftSide != point.GetLongitudeLeftSide)
                    {
                        compressedResult.AppendFormat("|lo{0}",
                            point.GetLongitudeLeftSide - previousPoint.GetLongitudeLeftSide);
                    }

                    compressedResult.AppendFormat("|{0}|{1}",
                        (point.GetLatitudeRightSide - previousPoint.GetLatitudeRightSide),
                        (point.GetLongitudeRightSide - previousPoint.GetLongitudeRightSide));
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

            var currentPoint = CreateGPSPoint(pointLatitudeLeftSide, pointLatitudeRightSide, pointLongitudeLeftSide,
                pointLongitudeRightSide);

            resultList.Add(currentPoint);

            for (var i = 4; i < desplittedArray.Length; i = i + 2)
            {

                if (desplittedArray[i].Contains("la"))
                {
                    var amount = desplittedArray[i].Replace("la", "");
                    pointLatitudeLeftSide = (int.Parse(pointLatitudeLeftSide) + int.Parse(amount)).ToString();
                    i++;
                }

                if (desplittedArray[i].Contains("lo"))
                {
                    var amount = desplittedArray[i].Replace("lo", "");
                    pointLongitudeLeftSide = (int.Parse(pointLongitudeLeftSide) + int.Parse(amount)).ToString();
                    i++;
                }

                var currentLatitudeRightSideChange = int.Parse(desplittedArray[i]);
                var currentLongitudeRightSideChange = int.Parse(desplittedArray[i + 1]);

                pointLatitudeRightSide = (int.Parse(pointLatitudeRightSide) + currentLatitudeRightSideChange).ToString();

                pointLongitudeRightSide =
                    (int.Parse(pointLongitudeRightSide) + currentLongitudeRightSideChange).ToString();

                var nextPoint = CreateGPSPoint(pointLatitudeLeftSide, pointLatitudeRightSide, pointLongitudeLeftSide,
                    pointLongitudeRightSide);

                resultList.Add(nextPoint);
            }

            return resultList;
        }

        private static GPS_Point CreateGPSPoint(string latitudeLeftSide, string latitudeRightSide,
            string longitudeLeftSide, string longitudeRightSide)
        {
            return new GPS_Point
            {
                Latitude = string.Format("{0}.{1}", latitudeLeftSide, Strings.PadWithZeroes(latitudeRightSide)),
                Longitude = string.Format("{0}.{1}", longitudeLeftSide, Strings.PadWithZeroes(longitudeRightSide))
            };
        }
    }
}