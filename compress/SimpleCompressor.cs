

namespace compress
{
    public class SimpleCompressor : PointCompressorAlgorithm
    {
        public override string CompressPoint(GPS_Point line)
        {

            return line.GetLatitudeLeftSide + line.GetLatitudeRightSide.ToString().PadLeft(GPS_Point.LengthOfRightSide, '0') +
                     line.GetLongitudeLeftSide + line.GetLongitudeRightSide.ToString().PadLeft(GPS_Point.LengthOfRightSide, '0');

        }
    }
}
