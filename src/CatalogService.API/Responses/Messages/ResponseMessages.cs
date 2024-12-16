using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Responses.Messages
{
    public enum ResponseMessages
    {
        [Description("Success: Operation is valid.")]
        VALID_OPERATION,
        [Description("Error: Operation is not valid.")]
        INVALID_OPERATION
    }
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

                if (attribute != null)
                    return attribute.Description;
            }

            return value.ToString();
        }
    }
}
