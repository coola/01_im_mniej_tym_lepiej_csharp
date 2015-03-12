

using System.Collections.Generic;

namespace compress
{
    public abstract class CompressorAlgorithm
    {
        protected  PointCompressorAlgorithm Algorithm
        {
            get; set; }

        protected CompressorAlgorithm(PointCompressorAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        public abstract string Compress(List<GPS_Point> points);
    }
}
