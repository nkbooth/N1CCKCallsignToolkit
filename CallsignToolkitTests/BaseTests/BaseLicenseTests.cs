using CallsignToolkit.CallbookLookup;
using CallsignToolkitTests.IBaseTests;

namespace CallsignToolkitTests.BaseTests
{
    public class BaseLicenseTests : IBaseLicenseTests
    {
        public BaseLicenseTests() : base(new License())
        {
        }
    }
}
