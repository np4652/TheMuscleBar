using System.ComponentModel;
using System.Reflection;

namespace TheMuscleBar.AppCode.Extensions
{
    public static class AnnotationExtention
    {
        public static string DescriptionAttribute<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi?.GetCustomAttributes(
                typeof(DescriptionAttribute), false) ?? new DescriptionAttribute[0];
            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source?.ToString() ?? "something went wrong";
        }
    }
}
