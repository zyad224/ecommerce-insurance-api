using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Domain.DomainExceptions
{
    public class InvalidSurchargeException:Exception
    {
        public InvalidSurchargeException(string message)
        : base(message)
        {
        }
    }
}
