namespace CallsignToolkit.Utilities
{
    public class Name
    {
        public virtual string FirstName
        {
            get => firstName;
            set
            {
                if (!string.IsNullOrEmpty(fullName) || fullName == "")
                {
                    firstName = value;
                }
                else
                {
                    throw new ArgumentException("Cannot set FirstName directly if FullName is already set");
                }

            }
        }
        public virtual string LastName
        {
            get => lastName;
            set
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    lastName = value;
                }
                else
                {
                    throw new ArgumentException("Cannot set LastName directly if FullName is already set");
                }
            }
        }

        public string MiddleInitial { get => middleInitial; set => middleInitial = value; }

        public virtual string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(fullName) || fullName == "")
                {
                    return fullName;
                }
                else
                {
                    return $"{FirstName} {LastName}";
                }
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
        public virtual string FullNameReverse
        {
            get
            {
                if (!string.IsNullOrEmpty(fullName) || fullName == "")
                {
                    return fullName;
                }
                else
                {
                    return $"{LastName}, {FirstName}";
                }
            }

        }

        // ReSharper disable once InconsistentNaming
        protected string fullName = string.Empty;
        // ReSharper disable once InconsistentNaming
        protected string firstName = string.Empty;
        // ReSharper disable once InconsistentNaming
        protected string lastName = string.Empty;
        // ReSharper disable once InconsistentNaming
        protected string middleInitial = string.Empty;


        public Name() { }
        public Name(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public static Name SeperateMiddleInitialFromFirstName(Name startingName)
        {
            if (!string.IsNullOrEmpty(startingName.MiddleInitial)) return startingName;
            
            string[] firstNameParts = startingName.FirstName.Split(' ');
            
            if (firstNameParts.Last().Length != 1) return startingName;
            
            startingName.MiddleInitial = firstNameParts.Last();
            startingName.FirstName = string.Join(" ", firstNameParts.Take(firstNameParts.Length - 1));
            
            return startingName;
        }
    }
}
