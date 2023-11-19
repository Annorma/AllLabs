using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generic.Extensions
{
    public static class DisplayActionExtensions
    {
        public static void Print<T>(this T obj)
        {
            Console.WriteLine(obj != null ? obj.ToString() : "null");
        }

        public static void Print<T>(this IList<T> list)
        {
            foreach (var item in list)
            {
                item.Print();
            }
        }
    }
}