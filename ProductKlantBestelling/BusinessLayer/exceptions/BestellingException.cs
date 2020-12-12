using System;
using System.Runtime.Serialization;

namespace BusinessLayer.model
{
    [Serializable]
    internal class BestellingException : Exception
    {
        public BestellingException()
        {
        }

        public BestellingException(string message) : base(message)
        {
        }

        public BestellingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BestellingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}