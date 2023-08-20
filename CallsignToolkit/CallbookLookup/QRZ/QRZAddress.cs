using CallsignToolkit.Utilities;
using System.Net.Mail;

namespace CallsignToolkit.CallbookLookup.QRZ
{
    public class QRZAddress : Address
    {
        public string Attention { get; set; } = string.Empty;

        [AddressOrder(45)]
        public string County { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public MailAddress? EmailAddress;
        public Uri? WebAddress;

        public QRZAddress() { }
        public QRZAddress(string addr1, string poBoxNum, string city, string state, string zipCode) : base(addr1, poBoxNum, city, state, zipCode)
        {
        }
        public QRZAddress(string addr1, string addr2, string poBoxNum, string city, string county, string state, string zipCode, string country) : base(addr1, addr2, poBoxNum, city, state, zipCode)
        {
            County = county;
            Country = country;
        }
    }
}
