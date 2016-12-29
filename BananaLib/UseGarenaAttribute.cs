
// Type: BananaLib.UseGarenaAttribute



using System;

namespace BananaLib
{
  [AttributeUsage(AttributeTargets.Field)]
  public class UseGarenaAttribute : Attribute
  {
    public bool Value { get; }

    public UseGarenaAttribute(bool value)
    {
      this.Value = value;
    }
  }
}
