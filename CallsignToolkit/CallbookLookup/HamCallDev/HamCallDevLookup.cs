using RestSharp;
using CallsignToolkit.Utilities;
using CallsignToolkit.Exceptions;

namespace CallsignToolkit.CallbookLookup.HamCallDev
{
    public class HamCallDevLookup : BaseLookup
    {
        public sealed override License License
        {
            get => hamCallDevLicense;
            protected set => hamCallDevLicense = (HamCallDevLicense)value;
        }
        private HamCallDevLicense hamCallDevLicense = new();

        [ResetOnClear()]
        public override Name AmateurName
        {
            get => hamCallDevName;
            set => hamCallDevName = (HamCallDevName)value;
        }
        private HamCallDevName hamCallDevName = new();

        [ResetOnClear()]
        public HamCallDevQSL? QSLMethods;

        private RestClient api = new RestClient("https://hamcall.dev/");

        public HamCallDevLookup() { }
        public HamCallDevLookup(string callsign)
        {
            HamCallDevLicense license = new(callsign);
            this.License = license;
        }

        public override async Task PerformLookup()
        {
            if (string.IsNullOrEmpty(this.License.Callsign) || License.Callsign == "")
            {
                throw new Exception("Callsign is required to perform lookup");
            }
            else
            {
                HamCallDevRawReturn results = await api.GetJsonAsync<HamCallDevRawReturn>($"{this.License.Callsign}.json");
                
                if (results.callsign == null || (string.IsNullOrEmpty(results.grant) && string.IsNullOrEmpty(results.effective) && string.IsNullOrEmpty(results.frn)))
                {
                    throw new CallsignNotFound(this.License.Callsign);
                }

                this.AmateurName = await GetNameFromResults(results);
                this.License = await GetLicenseFromResults(results);
                this.Address = await GetAddressFromResults(results);
                this.QSLMethods = await GetQSLMethodsFromResults(results);
            }
        }

        public override async Task PerformLookup(string callsign)
        {
            await this.ClearResults();
            this.License.Callsign = callsign;
            await this.PerformLookup();
        }

        public override async Task ClearResults()
        {
            api = new RestClient("https://hamcall.dev/");
            this.License = new HamCallDevLicense();
            await base.ClearResults();
        }

        private static Task<HamCallDevName> GetNameFromResults(HamCallDevRawReturn results)
        {
            HamCallDevName name = new();

            if (string.IsNullOrEmpty(results.first_name) && !string.IsNullOrEmpty(results.name))
            {
                name.FirstName = results.name;
            }
            else
            {
                name.FirstName = results.first_name;
                name.MiddleInitial = results.mi;
                name.LastName = results.last_name;
            }
            
            return Task.FromResult(name);
        }

        private Task<HamCallDevLicense> GetLicenseFromResults(HamCallDevRawReturn results)
        {
            HamCallDevLicense license = new()
            {
                Callsign = this.License.Callsign,
                LicenseClass = results.@class == "" ? "Club" : results.@class
            };

            DateTime.TryParse(results.grant, out license.GrantDate);
            DateTime.TryParse(results.effective, out license.EffectiveDate);
            DateTime.TryParse(results.expiration, out license.ExpirationDate);
            int.TryParse(results.frn, out license.FRN);
            int.TryParse(results.file_number, out license.FileNumber);
            int.TryParse(results.license_key, out license.LicenseKey);


            results.dmr_id.ForEach(x => license.DMRID.Add(x));

            return Task.FromResult(license);
        }

        private static Task<Address> GetAddressFromResults(HamCallDevRawReturn results) 
        {
            Address address = new()
            {
                Address1 = results.address,
                City = results.city,
                State = results.state,
                PostalCode = results.zip,
                POBoxNumber = results.po_box
            };

            address = Address.SanitizePostCode(address);
            return Task.FromResult(address);
        }

        private static Task<HamCallDevQSL> GetQSLMethodsFromResults(HamCallDevRawReturn results)
        {
            HamCallDevQSL qslMethods = new();
            DateTime.TryParse(results.last_lotw, out qslMethods.LastLOTWUpload);
            return Task.FromResult(qslMethods);
        }
    }
}
