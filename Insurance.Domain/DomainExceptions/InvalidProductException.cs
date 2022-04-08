using System;
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
