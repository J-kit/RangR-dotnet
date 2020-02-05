using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RangR.Exceptions
{
    [Serializable]
    public class OutOfRangeException : Exception
    {
        public OutOfRangeException()
        {
        }

        public OutOfRangeException(string message) : base(message)
        {
        }

        public OutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
