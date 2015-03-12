using System;
using System.Collections.Generic;
using System.Text;

namespace compress
{
    public class DeltaCompressorAlgorithm : CompressorAlgorithm
    {
        public DeltaCompressorAlgorithm(PointCompressorAlgorithm algorithm)
            : base(algorithm)
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
                    compressedResult.Append(Algorithm.CompressPoint(point));
                }
                else
                {

                    var previousPoint = points[index - 1];

                    compressedResult.Append( "|" + (point.GetLatitudeRightSide - previousPoint.GetLatitudeRightSide ) + "|" + (point.GetLongitudeRightSide - previousPoint.GetLongitudeRightSide));
                }
            }

            return compressedResult.ToString();
        }
    }
}