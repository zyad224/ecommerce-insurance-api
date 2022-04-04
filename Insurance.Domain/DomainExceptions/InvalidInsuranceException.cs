using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Domain.DomainExceptions
{
    public class InvalidInsuranceException:Exception
    {
        public InvalidInsuranceException(string message)
        : base(message)
        {
        }
    }
}
