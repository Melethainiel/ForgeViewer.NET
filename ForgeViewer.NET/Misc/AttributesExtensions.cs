using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using ForgeViewer.NET.Models;

namespace ForgeViewer.NET.Misc
{
    public static class AttributesExtensions
    {
        public static string DescriptionAttr<TSource>(this TSource source)
        {
            if (source is null || source.ToString() is not { } sourceName)
                return string.Empty;

            var fi = source.GetType().GetField(sourceName);
            if (fi is null)
                return sourceName;

            var attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : sourceName;
        }
        public static Type? TypeAttr<TSource>(this TSource source)
        {
            if (source is null || source.ToString() is not { } sourceName)
                return null;

            var fi = source.GetType().GetField(sourceName);

            var attributes = (EventResponseAttribute[]?) fi?.GetCustomAttributes(typeof(EventResponseAttribute), false);

            return attributes?.Length > 0 ? attributes[0].Type : null;
        }
        
        public static T GetValueFromDescription<T>(this string description) where T : Enum
        {
            foreach(var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)(field.GetValue(null) ?? throw new ArgumentException("Not found", nameof(description)));
                }
                else
                {
                    if (field.Name == description)
                        return (T)(field.GetValue(null) ?? throw new ArgumentException("Not found", nameof(description)));
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
        }
    }
}