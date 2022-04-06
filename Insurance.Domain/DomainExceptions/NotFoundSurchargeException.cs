using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Domain.DomainExceptions
{
    public class NotFoundSurchargeException: Exception
    {
        public NotFoundSurchargeException(string message)
        : base(message)
        {
        }
    }
}
