using System.Collections.Generic;

namespace ForgeViewer.NET.Misc
{
    static class DictionaryExtensions
    {
        public static T Get<T>(this IDictionary<string, object> dictionary, string key)
        {
            return (T) dictionary[key];
        }

        public static bool TryGet<T>(this IDictionary<string, object> dictionary,
            string key, out T? value)
        {
            if (dictionary.TryGetValue(key, out var result) && result is T result1)
            {
                value = result1;
                return true;
            }

            value = default;
            return false;
        }

        public static void Set(this IDictionary<string, object> dictionary,
            string key, object value)
        {
            dictionary[key] = value;
        }
    }
}