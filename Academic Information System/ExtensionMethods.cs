using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// nejaký komentár v branch 2

public static class ExtensionMethods
{
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
}

