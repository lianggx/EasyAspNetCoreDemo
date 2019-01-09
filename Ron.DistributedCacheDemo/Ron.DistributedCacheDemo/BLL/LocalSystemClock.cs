using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ron.DistributedCacheDemo.BLL
{
    public class LocalSystemClock : Microsoft.Extensions.Internal.ISystemClock
    {
        public DateTimeOffset UtcNow => DateTime.Now;
    }
}
