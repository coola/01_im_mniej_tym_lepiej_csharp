
using common;

namespace compressDecompress
{
    public abstract class PointAlgorithm
    {
        public abstract string CompressPoint(GPS_Point point);

        public abstract GPS_Point DecompressPoint(string point);
    }
}
