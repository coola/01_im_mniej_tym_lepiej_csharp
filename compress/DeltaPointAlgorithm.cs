using common;

namespace compress
{
    public class DeltaPointAlgorithm : PointAlgorithm
    {
        public override string CompressPoint(GPS_Point line)
        {
            return string.Format("{0}|{1}|{2}|{3}", line.GetLatitudeLeftSide, line.GetLatitudeRightSide, line.GetLongitudeLeftSide, line.GetLongitudeRightSide);
        }

        public override GPS_Point DecompressPoint(string point)
        {
            throw new System.NotImplementedException();
        }
    }
}