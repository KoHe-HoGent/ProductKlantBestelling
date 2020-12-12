using System;
using System.Runtime.Serialization;

namespace BusinessLayer.managers
{
    [Serializable]
    internal class ProductManagerException : Exception
    {
        public ProductManagerException()
        {
        }

        public ProductManagerException(string message) : base(message)
        {
        }

        public ProductManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}