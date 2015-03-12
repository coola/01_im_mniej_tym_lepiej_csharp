namespace compress
{
    public class DeltaPointCompressorAlgorithm : PointCompressorAlgorithm
    {
        public override string CompressPoint(GPS_Point line)
        {
            return string.Format("{0}|{1}|{2}|{3}", line.GetLatitudeLeftSide, line.GetLatitudeRightSide, line.GetLongitudeLeftSide, line.GetLongitudeRightSide);
        }
    }
}