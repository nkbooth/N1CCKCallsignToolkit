using CallsignToolkit.CallbookLookup;

namespace CallsignToolkitTests.IBaseTests
{
    public abstract class IBaseLicenseTests
    {
        protected readonly License license;
        protected IBaseLicenseTests(License license)
        {
            this.license = license;
        }

        [Fact]
        public void W1AWIsValidUSCallsign()
        {
            license.Callsign = "W1AW";
            Assert.True(license.IsValidUSCall());
        }

        [Fact]
        public void KN7LFGDIsNotValidUSCallsign()
        {
            license.Callsign = "KN7LFGD";
            Assert.False(license.IsValidUSCall());
        }

        [Fact]
        public void RandomStringIsNotValidUSCallsign()
        {
            license.Callsign = "aposi3df";
            Assert.False(license.IsValidUSCall());

            license.Callsign = "1234567890";
            Assert.True(!license.IsValidUSCall());

            license.Callsign = "h4ds";
            Assert.False(license.IsValidUSCall());
        }
    }
}
