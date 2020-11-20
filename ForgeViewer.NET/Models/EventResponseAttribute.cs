using System;

namespace ForgeViewer.NET.Models
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EventResponseAttribute : Attribute
    {
        public EventResponseAttribute(Type type)
        {
            Type = type;
        }

        public Type? Type { get; }
    }
}