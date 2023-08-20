using System.Net.Mail;
using RestSharp;
using CallsignToolkit.Utilities;
using CallsignToolkit.Exceptions;
using static System.Environment;

namespace CallsignToolkit.CallbookLookup.QRZ
{
    // ReSharper disable once InconsistentNaming
    public class QRZLookup : BaseLookup
    {
        [ResetOnClear()]
        public sealed override Name AmateurName
        {
            get => qrzName;
            set => qrzName = (QRZName)value;
        }
        private QRZName qrzName = new();

        #region Callsigns
        public sealed override License License 
        { 
            get => qrzLicense;
            protected set => qrzLicense = (QRZLicense)value;
        }
        private QRZLicense qrzLicense = new();

        [ResetOnClear()]
        public License PreviousCallsign = new();
        
        [ResetOnClear()]
        public List<License> Aliases = new();
        #endregion

        #region Locators
        [ResetOnClear()]
        public override Address Address
        {
            get => qrzAddress;
            protected set => qrzAddress = (QRZAddress)value;
        }
        private QRZAddress qrzAddress = new();

        [ResetOnClear()]
        public override Locators Locators { get => qrzLocators; set => qrzLocators = (QRZLocators)value; }
        private QRZLocators qrzLocators = new();
        #endregion

        #region QSL Details
        [ResetOnClear()]
        // ReSharper disable once InconsistentNaming
        public QSLMethods QSLMethods = new();
        #endregion

        #region Misc
        [ResetOnClear()]
        public Uri? ImageUrl;
        #endregion

        #region API Details
        private readonly string username;
        private readonly string password;
        private string sessionKey = string.Empty;
        private RestClient api = new RestClient(new RestClientOptions("https://xmldata.qrz.com/xml/1.31"));
        #endregion

        private bool disposedValue;

        #region Constructors
        public QRZLookup()
        {
            if (string.IsNullOrEmpty(GetEnvironmentVariable("QRZ_USER")))
            {
                throw new Exception("QRZ Username not set. Pass via string or set environment variable QRZ_USER");
            }
            else if (string.IsNullOrEmpty(GetEnvironmentVariable("QRZ_PASSWORD")))
            {
                throw new Exception("QRZ Password not set.  Pass via string or set environment variable QRZ_PASSWORD");
            }
            else
            {
                this.username = GetEnvironmentVariable("QRZ_USER") ?? string.Empty;
                this.password = GetEnvironmentVariable("QRZ_PASSWORD") ?? string.Empty;
                this.refreshSessionKey().Wait();   
            }
        }
        public QRZLookup(string callsign) : this()
        {
            License license = new QRZLicense()
            {
                Callsign = callsign
            };
            this.License = license;
        }
        public QRZLookup(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.refreshSessionKey().Wait();
        }
        public QRZLookup(string callsign, string username, string password)
        {
            License license = new License()
            {
                Callsign = callsign
            };
            this.License = license;
            this.username = username;
            this.password = password;
        }
        #endregion

        public override async Task PerformLookup()
        {
            if (this.sessionKey == string.Empty)
            {
                await refreshSessionKey();
            }

            RestRequest request = new RestRequest();
            request.AddOrUpdateParameter("s", this.sessionKey);
            request.AddOrUpdateParameter("callsign", this.License.Callsign);
            request.AddHeader("Content-Type", "application/xml");

            RestResponse<QRZDatabase> response = await api.ExecuteAsync<QRZDatabase>(request, Method.Get);
            if (response.IsSuccessful == false || response.Data == null)
            {
                throw new Exception("Error accessing QRZ API.", response.ErrorException);
            }
            else if (response.Data.Session.Error != string.Empty)
            {
                switch (response.Data.Session.Error)
                {
                    case { } c when c.Contains("Session Timeout"):
                        await refreshSessionKey();
                        await PerformLookup();
                        break;
                    case { } b when b.Contains("Invalid Session Key"):
                        await refreshSessionKey();
                        await PerformLookup();
                        break;
                    case { } a when a.Contains("Not found:"):
                        throw new CallsignNotFound(this.License.Callsign);                        
                    default:
                        throw new Exception("QRZ XML API: " + response.Data.Session.Error);
                }
            }
            else if (response.Data.Session.SubExp.ToLower() == "non-subscriber")
            {
                throw new Exception("QRZ XML API: Non-subscriber access denied.");
            }

            this.AmateurName = await GetAmateurNameFromResults(response.Data);
            this.License = await GetLicenseFromResults(response.Data);
            this.PreviousCallsign = await GetPreviousCallsignFromResults(response.Data);
            this.Aliases = await GetAliasesFromResults(response.Data);
            this.Address = await GetAddressFromResults(response.Data);
            this.QSLMethods = await GetQSLMethodsFromResults(response.Data);
            this.ImageUrl = await GetImageUrlFromResults(response.Data);
            this.Locators = await GetLocatorsFromResults(response.Data);
        }

        public override async Task PerformLookup(string callsign)
        {
            QRZLicense license = new QRZLicense
            {
                Callsign = callsign
            };
            this.License = license;
            await PerformLookup();
        }

        // ReSharper disable once InconsistentNaming
        private async Task refreshSessionKey()
        {
            RestRequest request = new RestRequest().AddParameter("username", this.username).AddParameter("password", this.password);
            request.AddHeader("Content-Type", "application/xml");
            
            RestResponse<QRZDatabase> response = await api.ExecuteAsync<QRZDatabase>(request, Method.Get);
            
            if (response.IsSuccessful == false)
            {
                throw new Exception("Error accessing QRZ API.", response.ErrorException);
            }
            else if (response.Data?.Session.SubExp.ToLower() == "non-subscriber")
            {
                throw new Exception("QRZ XML API: Non-subscriber access denied.");
            }
            this.sessionKey = response.Data?.Session.Key ?? string.Empty;
        }

        public override async Task ClearResults()
        {
            api = new RestClient(new RestClientOptions("https://xmldata.qrz.com/xml/1.31"));
            this.License = new QRZLicense();
            await base.ClearResults();
        }

        private static Task<QRZName> GetAmateurNameFromResults(QRZDatabase results)
        {
            QRZName qrzName = new()
            { 
                FirstName = results.Callsign.Fname,
                LastName = results.Callsign.Name,
                Nickname = results.Callsign.Nickname
            };

            Name parsedName = Name.SeperateMiddleInitialFromFirstName(qrzName);
            qrzName = (QRZName)parsedName;
            
            return Task.FromResult(qrzName);
        }
        
        private static Task<QRZLicense> GetLicenseFromResults(QRZDatabase results) 
        {
            QRZLicense license = new()
            { 
                Callsign = results.Callsign.Call,
                LicenseClass = results.Callsign.Class
            };

            DateTime.TryParse(results.Callsign.Efdate, out DateTime grantDate);
            license.GrantDate = grantDate;

            DateTime.TryParse(results.Callsign.Expdate, out DateTime expirationDate);
            license.ExpirationDate = expirationDate;
            
            return Task.FromResult(license);
        }

        private static Task<License> GetPreviousCallsignFromResults(QRZDatabase results)
        {
            License license = new()
            {
                Callsign = results.Callsign.P_call
            };
            return Task.FromResult(license);
        }

        private static Task<List<License>> GetAliasesFromResults(QRZDatabase results)
        {
            List<License> aliases = new();
            List<string> calls = results.Callsign.Aliases.Split(',').ToList();
            
            if (calls.Count > 0)
            {
                calls.ForEach(x => aliases.Add(new License(x)));   
            }
            
            return Task.FromResult(aliases);
        }

        private static Task<QRZAddress> GetAddressFromResults(QRZDatabase results)
        {
            QRZAddress address = new()
            {
                Attention = results.Callsign.Attn,
                Address1 = results.Callsign.Addr1,
                City = results.Callsign.Addr2,
                State = results.Callsign.State,
                PostalCode = results.Callsign.Zip,
                County = results.Callsign.County,
                Country = results.Callsign.Country
            };

            if(!string.IsNullOrEmpty(results.Callsign.Email))
            {
                address.EmailAddress = new MailAddress(results.Callsign.Email, results.Callsign.Name);
            }

            if (!string.IsNullOrEmpty(results.Callsign.Url))
            {
                address.WebAddress = new Uri(results.Callsign.Url);
            }

            address = (QRZAddress)Address.SanitizePostCode(address);
            return Task.FromResult(address);
        }

        // ReSharper disable once InconsistentNaming
        private static Task<QSLMethods> GetQSLMethodsFromResults(QRZDatabase results)
        {
            QSLMethods qslMethods = new()
            {
                UseEQSL = results.Callsign.Eqsl == "1",
                UseLOTW = results.Callsign.Lotw == "1",
                UsePaperQSL = results.Callsign.Mqsl == "1",
                QSLManager = results.Callsign.QSLMgr
            };

            return Task.FromResult(qslMethods);
        }

        private static Task<Uri> GetImageUrlFromResults(QRZDatabase results)
        {
            Uri image = results.Callsign.Image == string.Empty ? new Uri("about:blank") : new Uri(results.Callsign.Image);
            return Task.FromResult(image);
        }

        private static Task<QRZLocators> GetLocatorsFromResults(QRZDatabase results)
        {
            QRZLocators locators = new()
            {
                FIPSCode = results.Callsign.Fips,
                IOTA = results.Callsign.Iota,
                TeleAreaCode = results.Callsign.AreaCode,
                GeoCoordinates = new LatLong(results.Callsign.Lat, results.Callsign.Lon),
                GridSquare = results.Callsign.Grid
            };
            int.TryParse(results.Callsign.Dxcc, out int dxccNumber);
            locators.DXInformation = (DXInformation)dxccNumber;

            return Task.FromResult(locators);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposedValue) return;
            if (disposing)
            {
            }
            disposedValue = true;
        }
    }
}
