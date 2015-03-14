using System;
using common;

namespace compressDecompress
{
    public class SimplePointAlgorithm : PointAlgorithm
    {
        public override string CompressPoint(GPS_Point line)
        {

            return line.GetLatitudeLeftSide + line.GetLatitudeRightSide.ToString().PadLeft(GPS_Point.LengthOfRightSide, '0') +
                     line.GetLongitudeLeftSide + line.GetLongitudeRightSide.ToString().PadLeft(GPS_Point.LengthOfRightSide, '0');

        }

        public override GPS_Point DecompressPoint(string point)
        {
            throw new NotImplementedException();
        }
    }
}
