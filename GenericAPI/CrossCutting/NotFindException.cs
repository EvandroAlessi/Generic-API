using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GenericAPI.CrossCutting
{
    internal class NotFindException : SystemException
    {
        public NotFindException()
        {
        }

        public NotFindException(string message) : base(message)
        {
        }

        public NotFindException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFindException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}