namespace CallsignToolkit.Utilities
{
    public class QSLMethods
    {
        public string QSLManager 
        {
            get => qslManager;
            set => qslManager = value;
        }
        public bool UseEQSL 
        { 
            get => useEQSL;
            set => useEQSL = value;
        }
        public bool UseLOTW
        { 
            get => useLOTW;
            set => useLOTW = value;
        }

        public bool UsePaperQSL
        {
            get => usePaperQSL;
            set => usePaperQSL = value;
        }

        private string qslManager = string.Empty;
        private bool useEQSL = false;
        private bool useLOTW = false;
        private bool usePaperQSL = false;
    }
}
