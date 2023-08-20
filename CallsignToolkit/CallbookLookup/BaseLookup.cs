using Newtonsoft.Json;
using CallsignToolkit.Utilities;
using System.Reflection;
using RestSharp;

namespace CallsignToolkit.CallbookLookup
{
    public class BaseLookup : IDisposable, ICallbook
    {
        public virtual License License { get; protected set; } = new();

        [ResetOnClear()]
        public virtual Name AmateurName { get; set; } = new();

        [ResetOnClear()]
        public virtual Address Address { get; protected set; } = new();

        [ResetOnClear()] 
        public virtual Locators Locators { get; set; } = new();
        
        private readonly RestClient api = new();
        private bool disposedValue;

        public BaseLookup() { }
        public BaseLookup(string callsign)
        {
            throw new NotImplementedException();
        }
        
        public virtual async Task ClearResults()
        {
            if (this.GetType() == typeof(License))
            {
                this.License = new License();
            }
            
            IEnumerable<FieldInfo> fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToArray().Where(f => f.GetCustomAttribute<ResetOnClearAttribute>()?.ResetOnClear == true);                         
            foreach (FieldInfo field in fields.Where(f => f.GetCustomAttribute<ResetOnClearAttribute>()?.ResetOnClear == true))
            {
                field.SetValue(this, null);
            }            

            IEnumerable<PropertyInfo> properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToArray().Where(p => p.GetCustomAttribute<ResetOnClearAttribute>()?.ResetOnClear == true);
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(this, null);
            }
            
            await Task.CompletedTask;
        }

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue) return;
            if (disposing)
            {
            }
            api.Dispose();
            disposedValue = true;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual Task PerformLookup()
        {
            throw new NotImplementedException();
        }

        public virtual Task PerformLookup(string callsign)
        {
            throw new NotImplementedException();
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class ResetOnClearAttribute : Attribute
    {
        public bool ResetOnClear { get; set; }
        public ResetOnClearAttribute(bool resetOnClear = true)
        {
            this.ResetOnClear = resetOnClear;
        }
    }
}
