using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Logging
{
    public interface ILog
    {
        void Information(Exception e, string message);
        void Warning(Exception e, string message);
        void Debug(Exception e, string message);
        void Error(Exception e, string message);
    }
}
