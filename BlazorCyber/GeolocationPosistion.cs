namespace BlazorCyber
{
    public class GeolocationPosition
    {
        public GeolocationCoords Coords { get; set; }
    }

    public class GeolocationCoords
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
