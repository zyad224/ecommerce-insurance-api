using System;
namespace Insurance.Domain.DomainExceptions
{
    public class AlreadyExistSurchargeException:Exception
    {
        public AlreadyExistSurchargeException(string message)
        : base(message)
        {
        }
    }
}
