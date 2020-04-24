
// Type: BananaLib.LoginQueueAttribute



using System;

namespace BananaLib
{
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class LoginQueueAttribute : Attribute
  {
    public string Value { get; }

    public LoginQueueAttribute(string value)
    {
      this.Value = value;
    }
  }
}
