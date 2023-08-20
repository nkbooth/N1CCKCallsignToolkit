using CallsignToolkit.Utilities;

namespace CallsignToolkit.CallbookLookup.QRZ
{
    // ReSharper disable once InconsistentNaming
    public class QRZName : Name
    {
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public string Nickname { get; set; } = string.Empty;

        public override string FirstName 
        { 
            get
            {
                if(string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                    return lastName;
                else 
                    return firstName;
            }
            set => firstName = value; }
        public override string LastName 
        { 
            get
            {
                if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                    return string.Empty;
                else
                    return lastName;
            }
            set => lastName = value; }

        public override string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    return fullName;
                }
                else switch (string.IsNullOrEmpty(firstName))
                {
                    case false when string.IsNullOrEmpty(Nickname) && string.IsNullOrEmpty(lastName):
                        return firstName;
                    case false when !string.IsNullOrEmpty(Nickname) && !string.IsNullOrEmpty(lastName):
                        return $"{firstName} \"{Nickname}\" {lastName}";
                    case false when !string.IsNullOrEmpty(Nickname) && !string.IsNullOrEmpty(middleInitial) && !string.IsNullOrEmpty(lastName):
                        return $"{firstName} {middleInitial} \"{Nickname}\" {lastName}";
                    case false when string.IsNullOrEmpty(Nickname) && !string.IsNullOrEmpty(middleInitial) && !string.IsNullOrEmpty(lastName):
                        return $"{firstName} {middleInitial} {lastName}";
                    default:
                    {
                        if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(Nickname) && !string.IsNullOrEmpty(lastName))
                        {
                            return $"{lastName}";
                        }

                        break;
                    }
                }

                return string.Empty;
            }
        }
    }
}
