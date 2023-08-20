using System.Xml.Serialization;

namespace CallsignToolkit.CallbookLookup.QRZ
{
    [XmlRoot(ElementName = "Session", Namespace = "http://xmldata.qrz.com")]
    public class Session
    {

        [XmlElement(ElementName = "Key", Namespace = "http://xmldata.qrz.com")]
        public string Key { get; set; } = string.Empty;

        [XmlElement(ElementName = "Count", Namespace = "http://xmldata.qrz.com")]
        public int Count { get; set; }

        [XmlElement(ElementName = "SubExp", Namespace = "http://xmldata.qrz.com")]
        public string SubExp { get; set; } = string.Empty;

        [XmlElement(ElementName = "GMTime", Namespace = "http://xmldata.qrz.com")]
        public string GMTime { get; set; } = string.Empty;

        [XmlElement(ElementName = "Remark", Namespace = "http://xmldata.qrz.com")]
        public string Remark { get; set; } = string.Empty;

        [XmlElement(ElementName = "Error", Namespace = "http://xmldata.qrz.com")]
        public string Error { get; set; } = string.Empty;
    }

    [XmlRoot(ElementName = "QRZDatabase", Namespace = "http://xmldata.qrz.com")]
    public class QRZDatabase
    {

        [XmlElement(ElementName = "Session", Namespace = "http://xmldata.qrz.com")]
        public Session Session { get; set; } = new Session();

        [XmlElement(ElementName = "Callsign", Namespace = "http://xmldata.qrz.com")]
        public LookupResults Callsign { get; set; } = new LookupResults();

        [XmlAttribute(AttributeName = "version", Namespace = "")]
        public double Version { get; set; }

        [XmlAttribute(AttributeName = "xmlns", Namespace = "")]
        public string Xmlns { get; set; } = string.Empty;

        [XmlText] public string Text { get; set; } = string.Empty;
    }

    [XmlRoot(ElementName = "Callsign", Namespace = "http://xmldata.qrz.com")]
    public class LookupResults
    {
        [XmlElement(ElementName = "call", Namespace = "http://xmldata.qrz.com")]
        public string Call { get; set; } = string.Empty;

        [XmlElement(ElementName = "aliases", Namespace = "http://xmldata.qrz.com")]
        public string Aliases { get; set; } = string.Empty;

        [XmlElement(ElementName = "dxcc", Namespace = "http://xmldata.qrz.com")]
        public string Dxcc { get; set; } = string.Empty;

        [XmlElement(ElementName = "fname", Namespace = "http://xmldata.qrz.com")]
        public string Fname { get; set; } = string.Empty;

        [XmlElement(ElementName = "name", Namespace = "http://xmldata.qrz.com")]
        public string Name { get; set; } = string.Empty;

        [XmlElement(ElementName = "addr1", Namespace = "http://xmldata.qrz.com")]
        public string Addr1 { get; set; } = string.Empty;

        [XmlElement(ElementName = "addr2", Namespace = "http://xmldata.qrz.com")]
        public string Addr2 { get; set; } = string.Empty;

        [XmlElement(ElementName = "state", Namespace = "http://xmldata.qrz.com")]
        public string State { get; set; } = string.Empty;

        [XmlElement(ElementName = "zip", Namespace = "http://xmldata.qrz.com")]
        public string Zip { get; set; } = string.Empty;

        [XmlElement(ElementName = "country", Namespace = "http://xmldata.qrz.com")]
        public string Country { get; set; } = string.Empty;

        [XmlElement(ElementName = "lat", Namespace = "http://xmldata.qrz.com")]
        public string Lat { get; set; } = string.Empty;

        [XmlElement(ElementName = "lon", Namespace = "http://xmldata.qrz.com")]
        public string Lon { get; set; } = string.Empty;

        [XmlElement(ElementName = "grid", Namespace = "http://xmldata.qrz.com")]
        public string Grid { get; set; } = string.Empty;

        [XmlElement(ElementName = "county", Namespace = "http://xmldata.qrz.com")]
        public string County { get; set; } = string.Empty;

        [XmlElement(ElementName = "ccode", Namespace = "http://xmldata.qrz.com")]
        public string Ccode { get; set; } = string.Empty;

        [XmlElement(ElementName = "fips", Namespace = "http://xmldata.qrz.com")]
        public string Fips { get; set; } = string.Empty;

        [XmlElement(ElementName = "land", Namespace = "http://xmldata.qrz.com")]
        public string Land { get; set; } = string.Empty;

        [XmlElement(ElementName = "efdate", Namespace = "http://xmldata.qrz.com")]
        public string Efdate { get; set; } = string.Empty;

        [XmlElement(ElementName = "expdate", Namespace = "http://xmldata.qrz.com")]
        public string Expdate { get; set; } = string.Empty;

        [XmlElement(ElementName = "class", Namespace = "http://xmldata.qrz.com")]
        public string Class { get; set; } = string.Empty;

        [XmlElement(ElementName = "codes", Namespace = "http://xmldata.qrz.com")]
        public string Codes { get; set; } = string.Empty;

        [XmlElement(ElementName = "email", Namespace = "http://xmldata.qrz.com")]
        public string Email { get; set; } = string.Empty;

        [XmlElement(ElementName = "u_views", Namespace = "http://xmldata.qrz.com")]
        public string UViews { get; set; } = string.Empty;

        [XmlElement(ElementName = "bio", Namespace = "http://xmldata.qrz.com")]
        public string Bio { get; set; } = string.Empty;

        [XmlElement(ElementName = "biodate", Namespace = "http://xmldata.qrz.com")]
        public string Biodate { get; set; } = string.Empty;

        [XmlElement(ElementName = "moddate", Namespace = "http://xmldata.qrz.com")]
        public string Moddate { get; set; } = string.Empty;

        [XmlElement(ElementName = "MSA", Namespace = "http://xmldata.qrz.com")]
        public string MSA { get; set; } = string.Empty;

        [XmlElement(ElementName = "AreaCode", Namespace = "http://xmldata.qrz.com")]
        public string AreaCode { get; set; } = string.Empty;

        [XmlElement(ElementName = "TimeZone", Namespace = "http://xmldata.qrz.com")]
        public string TimeZone { get; set; } = string.Empty;

        [XmlElement(ElementName = "GMTOffset", Namespace = "http://xmldata.qrz.com")]
        public string GMTOffset { get; set; } = string.Empty;

        [XmlElement(ElementName = "DST", Namespace = "http://xmldata.qrz.com")]
        public string DST { get; set; } = string.Empty;

        [XmlElement(ElementName = "eqsl", Namespace = "http://xmldata.qrz.com")]
        public string Eqsl { get; set; } = string.Empty;

        [XmlElement(ElementName = "mqsl", Namespace = "http://xmldata.qrz.com")]
        public string Mqsl { get; set; } = string.Empty;

        [XmlElement(ElementName = "cqzone", Namespace = "http://xmldata.qrz.com")]
        public string Cqzone { get; set; } = string.Empty;

        [XmlElement(ElementName = "ituzone", Namespace = "http://xmldata.qrz.com")]
        public string Ituzone { get; set; } = string.Empty;

        [XmlElement(ElementName = "lotw", Namespace = "http://xmldata.qrz.com")]
        public string Lotw { get; set; } = string.Empty;

        [XmlElement(ElementName = "user", Namespace = "http://xmldata.qrz.com")]
        public string User { get; set; } = string.Empty;

        [XmlElement(ElementName = "geoloc", Namespace = "http://xmldata.qrz.com")]
        public string Geoloc { get; set; } = string.Empty;

        [XmlElement(ElementName = "nickname", Namespace = "http://xmldata.qrz.com")]
        public string Nickname { get; set; } = string.Empty;

        [XmlElement(ElementName = "p_call", Namespace = "http://xmldata.qrz.com")]
        public string P_call { get; set; } = string.Empty;

        [XmlElement(ElementName = "attn", Namespace = "http://xmldata.qrz.com")]
        public string Attn { get; set; } = string.Empty;

        [XmlElement(ElementName = "url", Namespace = "http://xmldata.qrz.com")]
        public string Url { get; set; } = string.Empty;

        [XmlElement(ElementName = "qslmgr", Namespace = "http://xmldata.qrz.com")]
        public string QSLMgr { get; set; } = string.Empty;

        [XmlElement(ElementName = "image", Namespace = "http://xmldata.qrz.com")]
        public string Image { get; set; } = string.Empty;

        [XmlElement(ElementName = "iota", Namespace = "http://xmldata.qrz.com")]
        public string Iota { get; set; } = string.Empty;
    }
}
