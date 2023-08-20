using CallsignToolkit.Utilities;

namespace CallsignToolkit.CallbookLookup.HamCallDev
{
    public class HamCallDevName : Name
    {
        public override string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    return fullName;
                }
                else if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(MiddleInitial) && !string.IsNullOrEmpty(lastName))
                {
                    return $"{FirstName} {MiddleInitial} {LastName}";
                }
                else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(MiddleInitial) && !string.IsNullOrEmpty(lastName))
                {
                    return $"{FirstName} {LastName}";
                }
                else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(MiddleInitial) && string.IsNullOrEmpty(lastName))
                {
                    return $"{FirstName}";
                }
                else return string.Empty;
            }

            set
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                {
                    fullName = value;
                }
                else
                {
                    throw new ArgumentException("Cannot set FullName directly if FirstName and LastName are already set");
                }
            }
        }
        public override string FirstName { get => firstName; set => firstName = value; }
        public override string LastName { get => lastName; set => lastName = value; }
    }
}
