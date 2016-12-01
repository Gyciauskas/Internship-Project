namespace PresentConnection.Internship7.Iot.Domain
{
    public class PowerResource
    {
        public PowerResource()
        {
            Type = PowerResourceType.NotSet;
        }

        public PowerResourceType Type { get; set; }
        public int Value { get; set; }
    }
}
