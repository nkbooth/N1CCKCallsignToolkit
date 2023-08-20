 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallsignToolkit.CallbookLookup.HamCallDev
{
    internal struct HamCallDevRawReturn
    {
        public string callsign { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string mi { get; set; }
        public string last_name { get; set; }
        public string @class { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string po_box { get; set; }
        public string grant { get; set; }
        public string effective { get; set; }
        public string expiration { get; set; }
        public string frn { get; set; }
        public string file_number { get; set; }
        public string last_lotw { get; set; }
        public string license_key { get; set; }
        public List<int> dmr_id { get => dmrids ?? new List<int>(); set => dmrids = value; }

        private List<int> dmrids;
    }
}