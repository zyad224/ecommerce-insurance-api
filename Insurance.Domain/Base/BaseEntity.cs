using System;
namespace Insurance.Domain.Base
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; protected set; }
        public DateTime ModifiedOn { get; protected set; }
    }
}
