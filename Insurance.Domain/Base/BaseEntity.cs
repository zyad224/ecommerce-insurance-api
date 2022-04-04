using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Domain.Base
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; protected set; }
        public DateTime ModifiedOn { get; protected set; }
    }
}
