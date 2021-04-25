using System;

namespace TPlayer.Web
{
    public class UUIDUtil
    {
        public static string genUUID()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Trim();
        }
    }
}
