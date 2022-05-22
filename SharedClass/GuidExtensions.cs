using System;

namespace SharedClass.Data
{
    public static class GuidExtensions
    {
        public static string ToURLGuid(this Guid guid)
        {
            return Convert.ToBase64String(guid.ToByteArray())
                .Replace("/", "-")
                .Replace("+", "_")
                .Replace("=", string.Empty);
        }

        public static Guid toGuidFromString(this string id)
        {
            var eff = Convert.FromBase64String(id
                .Replace("-", "/")
                .Replace("_", "+") + "==");
            return new Guid(eff);
        }
    }
}
