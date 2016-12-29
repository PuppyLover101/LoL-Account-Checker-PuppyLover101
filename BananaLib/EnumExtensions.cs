
// Type: BananaLib.EnumExtensions



using System;
using System.ComponentModel;
using System.Reflection;

namespace BananaLib
{
  public static class EnumExtensions
  {
    public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
      return (TAttribute) Attribute.GetCustomAttribute((MemberInfo) value.GetType().GetField(value.ToString()), typeof (TAttribute));
    }

    public static TAttribute[] GetAttributes<TAttribute>(this Enum value) where TAttribute : Attribute
    {
      return (TAttribute[]) Attribute.GetCustomAttributes((MemberInfo) value.GetType().GetField(value.ToString()), typeof (TAttribute));
    }

    public static string GetDescription(this Enum enumValue)
    {
      DescriptionAttribute attribute = enumValue.GetAttribute<DescriptionAttribute>();
      if (attribute == null)
        return string.Empty;
      return attribute.Description;
    }
  }
}
