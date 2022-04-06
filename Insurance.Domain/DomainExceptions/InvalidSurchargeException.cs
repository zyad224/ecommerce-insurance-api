using System;
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
