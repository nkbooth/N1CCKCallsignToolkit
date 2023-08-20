namespace CallsignToolkit.Exceptions
{
    public class CallsignNotFound : Exception
    {
        public CallsignNotFound() { }
        public CallsignNotFound(string callsign) : base($"Provided call sign not found: {callsign}")
        {
        }
    }
}
