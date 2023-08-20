namespace CallsignToolkit.CallbookLookup.QRZ
{
    public class QRZLicense : License
    {
        public DateTime GrantDate = new DateTime();
        public DateTime ExpirationDate = new DateTime();
        public string ServiceCodes = string.Empty;
    }
}
