using CallsignToolkitTests.IBaseTests;
using CallsignToolkit.CallbookLookup.HamCallDev;
using CallsignToolkit.CallbookLookup;

namespace CallsignToolkitTests.HamCallDevTests
{
    public class HamCallDevLookupTests : IBaseLookupTests
    {
        public HamCallDevLookupTests() : base(new HamCallDevLookup(), typeof(HamCallDevLookup))
        {
        }

        [Fact]
        public void CanSetCallsignInConstructor()
        {
            using (HamCallDevLookup lookup = new HamCallDevLookup("w1aw"))
            {
                HamCallDevLicense license = (HamCallDevLicense)lookup.License;
                Assert.Equal("w1aw", license.Callsign);
            }
        }
    }
}
