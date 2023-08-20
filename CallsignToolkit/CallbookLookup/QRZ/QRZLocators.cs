using CallsignToolkit.Utilities;

namespace CallsignToolkit.CallbookLookup.QRZ
{
    public class QRZLocators : Locators
    {

        public string FIPSCode { get; set; } = string.Empty;

        public string TeleAreaCode { get; set; } = string.Empty;
        public string IOTA { get; set; } = string.Empty;
    }
}