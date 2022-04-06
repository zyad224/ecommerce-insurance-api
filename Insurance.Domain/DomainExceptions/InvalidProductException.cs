using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Domain.DomainExceptions
{
    public class InvalidProductException:Exception
    {
        public InvalidProductException(string message)
        : base(message)
        {
        }
    }
}
