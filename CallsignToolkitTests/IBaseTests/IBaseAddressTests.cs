using CallsignToolkit.Utilities;

namespace CallsignToolkitTests.IBaseTests
{
    public abstract class IBaseAddressTests
    {
        public static IEnumerable<object[]> tempAddressesToTest =>
            new List<object[]>
            {
                new object[] {"225 Main St.", "Newington", "CT", "06111", 41.7146905, -72.72851776187042},
                new object[] { "PO Box 8172", "Manchester", "CT", "06040", 0,0 }
            };
        
        private Address address;
        protected IBaseAddressTests(Address addr)
        {
            address = addr;
        }

        [Theory]
        [MemberData(nameof(tempAddressesToTest))]
        public async Task AddressLatLong(string addr1, string city, string state, string postcode, double lat, double lon)
        {
            address.Address1 = addr1;
            address.City = city;
            address.State = state;
            address.PostalCode = postcode;

            var latLong = await Locators.GetLatLong(address);
            Assert.Equal(lat, latLong.Latitude);
            Assert.Equal(lon, latLong.Longitude);

            address = new();
        }
    }
}
