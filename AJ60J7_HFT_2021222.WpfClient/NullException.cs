using System;
using System.Runtime.Serialization;

namespace AJ60J7_HFT_2021222.WpfClient
{
    [Serializable]
    internal class NullException : Exception
    {
        public NullException() : base("Nincs kijelölt elem!") 
        {
        }
    }
}