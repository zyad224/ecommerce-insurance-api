using System;
namespace Insurance.Domain.Shared
{
    public static class UUIDGenerator
    {
        public static string NewUUID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
