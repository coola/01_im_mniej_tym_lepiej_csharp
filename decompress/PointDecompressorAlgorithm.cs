

using common;

namespace decompress
{
    public abstract class PointDecompressorAlgorithm
    {
        public abstract GPS_Point CompressPoint(string compressedPointString);
    }
}
