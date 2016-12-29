
// Type: BananaLib.FullNameAttribute



using System;

namespace BananaLib
{
  [AttributeUsage(AttributeTargets.Field)]
  public class FullNameAttribute : Attribute
  {
    public string Value { get; }

    public FullNameAttribute(string value)
    {
      this.Value = value;
    }
  }
}
