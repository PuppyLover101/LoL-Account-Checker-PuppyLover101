
// Type: BananaLib.LocaleAttribute



using System;

namespace BananaLib
{
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class LocaleAttribute : Attribute
  {
    public string Value { get; }

    public LocaleAttribute(string value)
    {
      this.Value = value;
    }
  }
}
