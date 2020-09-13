using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace bosch_api.Helper
{
    public static class ExtentionMethods
{
        public static string DisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();

            var enumString = enumType.GetField(enumValue.ToString()).Name;

            return (enumType.GetField(enumValue.ToString()).GetCustomAttribute<DisplayAttribute>()?.Name) ?? enumString;
        }

        public static DateTime ToTimezone(this DateTime utcTime, string toTimezone)
        {
            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(toTimezone);
                DateTime dt = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tz);
                dt = dt.AddMilliseconds(-dt.Millisecond);
                return dt;
            }
            catch (Exception)
            {
                // Not working somehow. DOn't know why Convert to Singapore time
                //return utcTime.AddHours(8);
                throw new Exception($"Cannot convert {utcTime} to {toTimezone}");
            }
        }
    }
}
