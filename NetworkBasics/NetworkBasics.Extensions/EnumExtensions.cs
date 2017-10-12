using System;
using System.ComponentModel;

namespace NetworkBasics.Extensions
{
    /// <summary>
    /// Extension methods to aid in the manipulation of enums.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/12/2017</Date>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the enum description.
        /// </summary>
        /// <param name="value">current enum value.</param>
        /// <returns>description.</returns>
        public static string ToDescription(this Enum value)
        {
            if (value != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attributes.Length > 0 ? attributes[0].Description : value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
