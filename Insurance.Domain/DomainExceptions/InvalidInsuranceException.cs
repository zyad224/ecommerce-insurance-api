using System;
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
