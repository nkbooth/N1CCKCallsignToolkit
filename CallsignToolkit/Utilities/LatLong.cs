namespace CallsignToolkit.Utilities
{
    public struct LatLong
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public LatLong() { }
        public LatLong(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public LatLong(string lat, string lon)
        {
            Latitude = double.Parse(lat);
            Longitude = double.Parse(lon);
        }
    }
}
