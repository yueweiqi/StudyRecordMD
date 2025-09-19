using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.Extension
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum enumValue)
        {
            DescriptionAttribute[] array = enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false) as DescriptionAttribute[];
            if (array.Length == 0)
            {
                return enumValue.ToString();
            }

            return array[0].Description;
        }
    }
}
