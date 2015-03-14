using System;

namespace common
{
    public class GPS_Point
    {
        public const int LengthOfRightSide = 6;

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public int GetLatitudeLeftSide
        {
            get
            {
                return Int32.Parse(Latitude.Split('.')[0]);
            }
        }

        public int GetLatitudeRightSide
        {
            get
            {
                return Int32.Parse(Latitude.Split('.')[1]);
            }
        }

        public int GetLongitudeLeftSide
        {
            get
            {
                 return Int32.Parse(Longitude.Split('.')[0]);
            }
        }

        public int GetLongitudeRightSide
        {
            get
            {
                 return Int32.Parse(Longitude.Split('.')[1]);
            }
        }
    }
}
