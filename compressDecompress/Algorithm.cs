using System.Collections.Generic;
using common;

namespace compressDecompress
{
    public abstract class Algorithm
    {
        protected  PointAlgorithm InnerAlgorithm
        {
            get; set; 
        }

        protected Algorithm(PointAlgorithm algorithm)
        {
            InnerAlgorithm = algorithm;
        }

        public abstract string Compress(List<GPS_Point> points);

        public abstract List<GPS_Point> Decompress(string compressedString);
        
    }
}
