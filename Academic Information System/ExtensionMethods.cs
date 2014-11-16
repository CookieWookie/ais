using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

public static class ExtensionMethods
{
	// comment
    public static void ThrowIfNull<T>(this T source)
    {
        if (ReferenceEquals(source, null))
        {
            throw new ArgumentNullException();
        }
    }

    public static void ThrowIfNull<T>(this T source, string paramName)
    {
        if (ReferenceEquals(source, null))
        {
            throw new ArgumentNullException(paramName);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        source.ThrowIfNull("source");
        action.ThrowIfNull("action");
        foreach (T element in source)
        {
            action(element);
        }
    }

    public static string GetDescription(this Enum enumObj)
    {
        FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

        object[] attribArray = fieldInfo.GetCustomAttributes(false);

        if (attribArray.Length == 0)
        {
            return enumObj.ToString();
        }
        else
        {
            DescriptionAttribute attrib = attribArray[0] as DescriptionAttribute;
            return attrib.Description;
        }
    }
}

