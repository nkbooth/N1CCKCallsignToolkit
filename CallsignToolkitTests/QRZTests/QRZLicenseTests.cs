using CallsignToolkitTests.IBaseTests;
using CallsignToolkit.CallbookLookup.QRZ;

namespace CallsignToolkitTests.QRZTests
{
    public class QRZLicenseTests : IBaseLicenseTests
    {
        public QRZLicenseTests() : base(new QRZLicense())
        {
        }
    }
}
