using System.Collections.Generic;
using System.Text;
using common;

namespace compress
{
    public class DeltaAlgorithm : Algorithm
    {
        public DeltaAlgorithm(PointAlgorithm decompressorAlgorithm)
            : base(decompressorAlgorithm)
        {
        }

        public override string Compress(List<GPS_Point> points)
        {
            StringBuilder compressedResult = new StringBuilder();

            for (int index = 0; index < points.Count; index++)
            {
                var point = points[index];

                if (index == 0)
                {
                    compressedResult.Append(InnerAlgorithm.CompressPoint(point));
                }
                else
                {

                    var previousPoint = points[index - 1];

                    compressedResult.Append( "|" + (point.GetLatitudeRightSide - previousPoint.GetLatitudeRightSide ) + "|" + (point.GetLongitudeRightSide - previousPoint.GetLongitudeRightSide));
                }
            }

            return compressedResult.ToString();
        }

        public override List<GPS_Point> Decompress(string compressedString)
        {

            var resultList = new List<GPS_Point>();

            var splittedString = compressedString.Split('|');

            return resultList;

        }
    }
}