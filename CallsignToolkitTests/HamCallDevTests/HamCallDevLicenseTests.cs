using CallsignToolkitTests.IBaseTests;
using CallsignToolkit.CallbookLookup.HamCallDev;
using CallsignToolkit.Utilities;

namespace CallsignToolkitTests.HamCallDevTests
{
    public class HamCallDevLicenseTests : IBaseLicenseTests
    {
        public HamCallDevLicenseTests() : base(new HamCallDevLicense())
        {
        }

        [Fact]
        public async void DatesAreCorrect()
        {
            using (HamCallDevLookup lookup = new HamCallDevLookup("w1aw"))
            {
                await lookup.PerformLookup();
                HamCallDevLicense license = (HamCallDevLicense)lookup.License;
                Assert.Equal(new DateTime(2020, 12, 08), license.GrantDate);
                Assert.Equal(new DateTime(2020, 12, 08), license.EffectiveDate);
                Assert.Equal(new DateTime(2031, 02, 26), license.ExpirationDate);
            }
        }

        [Fact]
        public async void W1AWHasTwoDMRIds()
        {
            using (HamCallDevLookup lookup = new HamCallDevLookup("w1aw"))
            {
                await lookup.PerformLookup();
                HamCallDevLicense license = (HamCallDevLicense)lookup.License;
                Assert.Equal(2, license.DMRID.Count);
                Assert.Equal(310938, license.DMRID[1]);
            }
        }

        [Fact]
        public async void DMRIDLookup()
        {
            using (HamCallDevLookup lookup = new HamCallDevLookup())
            {
                await lookup.PerformLookup("n1cck");
                HamCallDevLicense license = (HamCallDevLicense)lookup.License;
                Assert.Equal(3168930, license.DMRID[0]);

                await lookup.PerformLookup("KB3JZU");
                license = (HamCallDevLicense)lookup.License;
                Assert.Empty(license.DMRID);
            }
        }

        [Fact]
        public async void FRNIsCorrect()
        {
            using (HamCallDevLookup lookup = new HamCallDevLookup("w1aw"))
            {
                await lookup.PerformLookup();
                HamCallDevLicense license = (HamCallDevLicense)lookup.License;
                Assert.Equal(0004511143, license.FRN);
            }
        }
    }
}
