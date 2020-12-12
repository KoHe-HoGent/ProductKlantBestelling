using System;
using System.Runtime.Serialization;

namespace BusinessLayer.model
{
    [Serializable]
    internal class ProductException : Exception
    {
        public ProductException()
        {
        }

        public ProductException(string message) : base(message)
        {
        }

        public ProductException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}