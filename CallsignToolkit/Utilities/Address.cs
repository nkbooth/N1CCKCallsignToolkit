namespace CallsignToolkit.Utilities
{
    public class Address : IDisposable
    {
        [AddressOrder(10)]
        public string Address1
        {
            get
            {
                if (!string.IsNullOrEmpty(poBoxNumber))
                {
                    return string.IsNullOrEmpty(address1) ? $"PO Box {poBoxNumber}" : $"PO Box {poBoxNumber}, {address1}";
                }
                else
                {
                    return address1 ?? string.Empty;
                }
            }
            set => address1 = value;
        }
        [AddressOrder(20)]
        public string Address2 { get => address2 ?? string.Empty;
            set => address2 = value;
        }
        [AddressOrder(30)]
        public string POBoxNumber { get => poBoxNumber;
            set => poBoxNumber = value ?? string.Empty;
        }
        [AddressOrder(40)]
        public string City { get => city ?? string.Empty;
            set => city = value;
        }
        [AddressOrder(50)]
        public string State { get => state ?? string.Empty;
            set => state = value;
        }
        [AddressOrder(60)]
        public string PostalCode { get => postalCode ?? string.Empty;
            set => postalCode = value;
        }


        private string? address1;
        private string? address2;
        private string? poBoxNumber;
        private string? city;
        private string? state;
        private string? postalCode;
        private bool disposedValue;

        public Address() { }

        protected Address(string addr1, string addr2, string city, string state, string zipCode)
        {
            this.Address1 = addr1;
            this.Address2 = addr2;
            this.City = city;
            this.State = state;
            this.PostalCode = zipCode;
        }
        public Address(string poBoxNum, string city, string state, string zipCode)
        {
            this.POBoxNumber = poBoxNum;
            this.City = city;
            this.State = state;
            this.PostalCode = zipCode;
        }

        protected Address(string addr1, string addr2, string poBoxNum, string city, string state, string zipCode)
        {
            this.Address1 = addr1;
            this.Address2 = addr2;
            this.POBoxNumber = poBoxNum;
            this.City = city;
            this.State = state;
            this.PostalCode = zipCode;
        }

        public static Address SanitizePostCode(Address addr)
        {
            if (string.IsNullOrEmpty(addr.postalCode)) return addr;
            addr.postalCode.Replace("-", "");
            if (!addr.postalCode.All(c => c is >= '0' and <= '9')) return addr;
            if(addr.postalCode.Length > 5)
                addr.postalCode = addr.postalCode.Insert(5, "-");
            return addr;
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue) return;
            if (disposing)
            {
                    
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class AddressOrderAttribute : Attribute
    {
        public AddressOrderAttribute(int order)
        {
            this.Order = order;
        }
        public int Order { get; set; }
    }
}
