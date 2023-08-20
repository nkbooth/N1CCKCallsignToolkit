using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CallsignToolkit.Utilities;

namespace CallsignToolkit.CallbookLookup
{
    public class License
    {
        public string Callsign { get; set; } = string.Empty;

        public string LicenseClass
        {
            get 
            { 
                switch(licenseClass.ToLower())
                {
                    case "t":
                    case "/kt":
                    case "kt":
                    case "tech":
                    case "technician": 
                        return "Technician";
                    
                    case "g":
                    case "gen":
                    case "/ag":
                    case "ag":
                    case "general":
                        return "General";
                    
                    case "e":
                    case "ae":
                    case "extra":
                        return "Amateur Extra";
                    
                    case "a":
                    case "advance":
                    case "advanced":
                        return "Advanced";
                    
                    case "n":
                    case "novice":
                        return "Novice";
                    case "c":
                    case "club":
                        return "Club";
                    
                    default: return licenseClass;
                }
            }
            set => licenseClass = value;
        }

        private string licenseClass = string.Empty;

        private static readonly string[] USCallFilterRules = 
        {
            "(?:[KNW][0-9][A-Z]{3})",
            "(?:[KW][A-Z][0-9][A-Z]{3})",
            "(?:[KNW][A-Z]?[0-9][A-Z]{1,2})",
            "(?:(?:(?:A[A-L])|(?:[KNW][A-Z]))[0-9][A-Z]{1,2})",
            "(?:(?:(?:[KNW]L)|(?:[NW]P)|(?:[KNW]H))[0-9][A-Z]{2})"
        };

        #region constructors
        public License() { }
        public License (string call)
        {
            this.Callsign = call;
        }
        public License(string call, string licenseClass)
        {
            this.Callsign = call;
            this.licenseClass = licenseClass;
            
        }
        #endregion

        public bool IsValidUSCall()
        {
            if (string.IsNullOrEmpty(Callsign) || Callsign == "")
            {
                throw new ArgumentException("Callsign cannot be null or empty");
            }

            if (Callsign.Length > 6 || Callsign.Length < 3)
            {
                return false;
            }
            else
            {
                string callFilterRegex = string.Join("|", USCallFilterRules);
                return Regex.IsMatch(Callsign.ToUpper(), callFilterRegex);
            }
        }
    }
}
