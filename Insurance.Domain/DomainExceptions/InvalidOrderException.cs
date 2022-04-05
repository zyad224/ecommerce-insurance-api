using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Domain.DomainExceptions
{
    public class InvalidOrderException: Exception
    {
        public InvalidOrderException(string message)
        : base(message)
        {
        }
    }
}
