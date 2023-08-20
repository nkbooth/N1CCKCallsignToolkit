using CallsignToolkit.CallbookLookup;
using CallsignToolkit.CallbookLookup.HamCallDev;
using CallsignToolkit.CallbookLookup.QRZ;
using CallsignToolkit.Exceptions;
using System.Diagnostics.Contracts;

namespace CallsignToolkitTests.IBaseTests
{
    public abstract class IBaseLookupTests
    {
        private BaseLookup callbook;
        private Type callbookType;
        protected IBaseLookupTests(BaseLookup lookup, Type callbookType)
        {
            this.callbook = lookup;
            this.callbookType = callbookType;
        }
        
        [Fact]
        public async void CanSetCallsignInProperty()
        {
            callbook.License.Callsign = "w1aw";
            Assert.Equal("w1aw", callbook.License.Callsign);
            await this.callbook.ClearResults();
        }

        [Theory]
        [InlineData("n1cck", "Nicholas K Booth")]
        [InlineData("w1aw", "ARRL HQ OPERATORS CLUB")]
        public virtual async void PerformLookupFullNameMatches(string callsign, string fullName)
        {
            try
            {
                await this.callbook.PerformLookup(callsign);
                Assert.Equal(fullName, this.callbook.AmateurName.FullName);
                await this.callbook.ClearResults();
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook.GetType() == typeof(BaseLookup));
            }
        }

        [Theory]
        [InlineData("n1cck", "Nicholas")]
        [InlineData("w1aw", "ARRL HQ OPERATORS CLUB")]
        [InlineData("AA0JY", "Walter N")]
        [InlineData("N6FW", "Kurt A")]
        public virtual async void PerformLookupFirstNameMatches(string callsign, string firstName)
        {
            try
            {
                await this.callbook.PerformLookup(callsign);
                Assert.Equal(firstName.ToLower(), this.callbook.AmateurName.FirstName.ToLower());
                await this.callbook.ClearResults();
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook.GetType() == typeof(BaseLookup));
            }
        }

        [Theory]
        [InlineData("n1cck", "booth")]
        [InlineData("w1aw", "")]
        public async void PerformLookupLastNameMatches(string callsign, string lastName)
        {
            try
            {
                await callbook.PerformLookup(callsign);
                Assert.Equal(lastName.ToLower(), callbook.AmateurName.LastName.ToLower());
                await callbook.ClearResults();
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook.GetType() == typeof(BaseLookup));
            }
        }

        [Theory]
        [InlineData("n1cck", "K")]
        [InlineData("w1aw", "")]
        [InlineData("AA0JY", "A")]
        [InlineData("N6FW", "F")]
        public async void PerformLookupMiddleInitialMatches(string callsign, string middleInitial)
        {
            try
            {
                await callbook.PerformLookup(callsign);
                Assert.Equal(middleInitial.ToLower(), callbook.AmateurName.MiddleInitial.ToLower());
                await callbook.ClearResults();
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook.GetType() == typeof(BaseLookup));
            }
        }

        [Theory]
        [InlineData("n1cck", "PO Box 8172")]
        [InlineData("w1aw", "225 Main St")]
        [InlineData("M0JEO", "")]
        public async void PerformLookupAddress1Matches(string callsign, string addr1)
        {
            try
            {
                await callbook.PerformLookup(callsign);
                Assert.Equal(addr1.ToUpper(), callbook.Address.Address1.ToUpper());
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook?.GetType() == typeof(BaseLookup));
            }
            catch (CallsignNotFound)
            {
                Assert.True(callbook.GetType() == typeof(HamCallDevLookup));
            }
        }

        [Theory]
        [InlineData("n1cck", "")]
        [InlineData("w1aw", "")]
        [InlineData("M0JEO", "")]
        public async void PerformLookupAddress2Matches(string callsign, string addr2)
        {
            try
            {
                await callbook.PerformLookup(callsign);
                Assert.Equal(addr2.ToUpper(), callbook.Address.Address2.ToUpper());
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook?.GetType() == typeof(BaseLookup));
            }
            catch (CallsignNotFound)
            {
                Assert.True(callbook.GetType() == typeof(HamCallDevLookup));
            }
        }

        [Theory]
        [InlineData("n1cck", "Manchester")]
        [InlineData("w1aw", "Newington")]
        [InlineData("M0JEO", "Hartshorne")]
        public async void PerformLookupCityMatches(string callsign, string city)
        {
            try
            {
                await callbook.PerformLookup(callsign);
                Assert.Equal(city.ToUpper(), callbook.Address.City.ToUpper());
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook?.GetType() == typeof(BaseLookup));
            }
            catch (CallsignNotFound)
            {
                Assert.True(callbook?.GetType() == typeof(HamCallDevLookup));
            }
        }

        [Theory]
        [InlineData("n1cck", "CT")]
        [InlineData("w1aw", "CT")]
        [InlineData("M0JEO", "")]
        public async void PerformLookupStateMatches(string callsign, string state)
        {
            try
            {
                await callbook.PerformLookup(callsign);
                Assert.Equal(state.ToUpper(), callbook.Address.State.ToUpper());
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook?.GetType() == typeof(BaseLookup));
            }
            catch (CallsignNotFound)
            {
                Assert.True(callbook?.GetType() == typeof(HamCallDevLookup));
            }
        }

        [Theory]
        [InlineData("n1cck", "06040-5447")]
        [InlineData("w1aw", "06111")]
        [InlineData("M0JEO", "DE11 7EZ")]
        public async void PerformLookupZipMatches(string callsign, string zip)
        {
            try
            {
                await callbook.PerformLookup(callsign);
                Assert.Equal(zip.ToUpper(), callbook.Address.PostalCode.ToUpper());
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook?.GetType() == typeof(BaseLookup));
            }
            catch (CallsignNotFound)
            {
                Assert.True(callbook?.GetType() == typeof(HamCallDevLookup));
            }
        }

        [Fact]
        public async Task ClearRemovesDetails()
        {
            try
            {
                await this.callbook.PerformLookup("W1AW");
                await this.callbook.ClearResults();
                Assert.Null(this.callbook.AmateurName);
                Assert.Null(this.callbook.Address);
                Assert.Equal("", this.callbook.License.Callsign);
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook.GetType() == typeof(BaseLookup));
            }
        }

        [Fact]
        public async Task ClearIsReusable()
        {
            try
            {
                var changedCallbook = this.callbook;
                Convert.ChangeType(changedCallbook, callbookType);
                await changedCallbook.PerformLookup("n1cck");
                await changedCallbook.ClearResults();
                await changedCallbook.PerformLookup("W1AW");
                Assert.Equal("ARRL HQ OPERATORS CLUB", this.callbook.AmateurName.FullName);
            }
            catch (NotImplementedException)
            {
                Assert.True(callbook.GetType() == typeof(BaseLookup));
            }
        }

        [Fact]
        public async void ClubCallsNameCorrectly()
        {
            try 
            {
                await callbook.PerformLookup("w1aw");
                Assert.Equal("ARRL HQ OPERATORS CLUB", callbook.AmateurName.FullName);
            }
            catch (NotImplementedException) { Assert.True(callbook.GetType() == typeof(BaseLookup)); }            
        }

        [Fact]
        public async void CallsignNotFoundHandled()
        {
            try
            {
                if (callbook.GetType() != typeof(BaseLookup))
                    await Assert.ThrowsAsync<CallsignNotFound>(() => callbook.PerformLookup("n45tf"));
            }
            catch (NotImplementedException) { Assert.True(callbook.GetType() == typeof(BaseLookup)); }
        }

        [Theory]
        [InlineData("n1cck", "Amateur Extra")]
        [InlineData("ka0bog", "General")]
        [InlineData("ka3ifx", "Advanced")]
        [InlineData("KB3JZU", "Technician")]
        [InlineData("W1AW", "Club")]
        public virtual async void LicenseClassSetCorrectly(string callsign, string licenseClass)
        {
            try
            {
                await callbook.PerformLookup(callsign);
                Convert.ChangeType(callbook, callbookType);
                Assert.Equal(licenseClass, callbook.License.LicenseClass);
            }
            catch (NotImplementedException) { Assert.True(callbook.GetType() == typeof(BaseLookup)); }
        }
    }
}
