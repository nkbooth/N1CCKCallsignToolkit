namespace CallsignToolkit.CallbookLookup
{
    public interface ICallbook
    {
        public abstract Task PerformLookup();
        public abstract Task PerformLookup(string callsign);
        public abstract Task ClearResults();
    }
}