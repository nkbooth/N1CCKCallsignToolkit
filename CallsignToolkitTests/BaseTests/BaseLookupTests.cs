using CallsignToolkit;
using CallsignToolkit.CallbookLookup;
using CallsignToolkitTests.IBaseTests;

namespace CallsignToolkitTests.BaseTests
{
    public class BaseLookupTests : IBaseLookupTests
    {
        public BaseLookupTests() : base(new BaseLookup(), typeof(BaseLookup))
        { }
    }
}
