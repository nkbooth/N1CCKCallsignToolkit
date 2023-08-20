using System.Reflection;
using CallsignToolkit.CallbookLookup.QRZ;
using CallsignToolkit.Utilities;
using CallsignToolkitTests.IBaseTests;

namespace CallsignToolkitTests.QRZTests
{
    public class QRZLookupTests : IBaseLookupTests
    {
        public QRZLookupTests() : base(new QRZLookup(), typeof(QRZLookup))
        {
        }

        [Fact]
        public void CanSetCallsignInConstructor()
        {
            using (QRZLookup lookup = new QRZLookup("w1aw"))
            {
                Assert.Equal("w1aw", lookup.License.Callsign);
            }
        }

        [Theory]
        [InlineData("n1cck", "kc1mcn")]
        [InlineData("w1aw", "")]
        public async void AliasesSetCorrectly(string callsign, string alias)
        {
            using (QRZLookup callbook = new QRZLookup())
            {
                await callbook.PerformLookup(callsign);
                Assert.NotEmpty(callbook.Aliases);
                Assert.Equal(alias, callbook.Aliases[0].Callsign.ToLower());
            }
        }

        [Theory]
        [InlineData("w1aw")]
        public async void NoAliasesIsEmpty(string callsign)
        {
            using (QRZLookup callbook = new QRZLookup(callsign))
            {
                Assert.Empty(callbook.Aliases);
            }
        }

        [Theory]
        [InlineData("n1cck", @"nick@n1cck.us")]
        [InlineData("w1aw", @"w1aw@arrl.org")]
        public async void EmailAddressCorrect(string callsign, string email)
        {
            using (QRZLookup callbook = new QRZLookup())
            {
                await callbook.PerformLookup(callsign);
                var addr = (QRZAddress)callbook.Address;
                Assert.Equal(email.ToLower(), addr.EmailAddress.Address.ToLower());
            }
        }

        [Fact]
        public async void ImageAddressLookup()
        {
            using (QRZLookup lookup = new QRZLookup("W1AW"))
            {
                await lookup.PerformLookup();
                Assert.Equal("http://files.qrz.com/w/w1aw/Large_W1AW_Image.jpg", lookup.ImageUrl.AbsoluteUri.ToString());
            }
        }

        [Theory]
        [InlineData("w1aw", "09003","","FN31PR")]
        [InlineData("n1cck", "09003", "", "FN31RS")]
        public async void MiscLocatorsCorrect(string callsign, string fips, string iota, string grid)
        {
            using (QRZLookup lookup = new())
            {
                await lookup.PerformLookup(callsign);
                QRZLocators loc = (QRZLocators)lookup.Locators;
                Assert.Equal(fips, loc.FIPSCode);
                Assert.Equal(iota, loc.IOTA);
                Assert.Equal(grid, loc.GridSquare.ToUpper());
            }
        }

        [Theory]
        [InlineData("n1cck", DXInformation.UnitedStatesofAmerica, "NA", "US", "United States of America")]
        [InlineData("w1aw", DXInformation.UnitedStatesofAmerica, "NA", "US", "United States of America")]
        [InlineData("GB2RS", DXInformation.England, "EU", "GB", "England")]
        public async void DXCCInformationCorrect(string callsign, DXInformation dxInformation, string continent, string isoCountryCode, string countryName)
        {
            using (QRZLookup lookup = new())
            {
                await lookup.PerformLookup(callsign);
                Assert.Equal(lookup.Locators.DXInformation, dxInformation);
                Assert.Equal(continent, lookup.Locators.DXInformation.GetCustomAttribute<ContinentNameAttribute>().ContinentName);
                Assert.Equal(isoCountryCode, lookup.Locators.DXInformation.GetCustomAttribute<ISOCountryCodeAttribute>().ISOCountryCode);
                Assert.Equal(countryName, lookup.Locators.DXInformation.GetCustomAttribute<FriendlyNameAttribute>().FriendlyName);
            }
        }
        
        [Theory]
        [InlineData("n1cck", 41.762370, -72.520810)]
        [InlineData("w1aw", 41.714775, -72.727260)]
        public async void LatLongSetCorrectly(string callsign, double lat, double lon)
        {
            using (QRZLookup lookup = new QRZLookup())
            {
                await lookup.PerformLookup(callsign);
                Assert.Equal(lat, lookup.Locators.GeoCoordinates.Latitude);
                Assert.Equal(lon, lookup.Locators.GeoCoordinates.Longitude);
            }
        }
    }
}