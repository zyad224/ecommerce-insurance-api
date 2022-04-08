using System;
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
