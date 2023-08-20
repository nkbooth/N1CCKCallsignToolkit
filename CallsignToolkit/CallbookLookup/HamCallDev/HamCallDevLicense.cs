namespace CallsignToolkit.CallbookLookup.HamCallDev
{
    public class HamCallDevLicense : License
    {
        public List<int> DMRID = new List<int>();
        public int FRN;
        public int FileNumber;
        public int LicenseKey;

        public DateTime GrantDate;
        public DateTime EffectiveDate;
        public DateTime ExpirationDate;

        public HamCallDevLicense() { }
        public HamCallDevLicense(string callsign) : base(callsign) { }
    }
}