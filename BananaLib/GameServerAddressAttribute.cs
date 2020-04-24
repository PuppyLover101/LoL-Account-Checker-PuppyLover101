
// Type: BananaLib.GameServerAddressAttribute



using System;

namespace BananaLib
{
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
  public class GameServerAddressAttribute : Attribute
  {
    public string Value { get; }

    public GameServerAddressAttribute(string value)
    {
      this.Value = value;
    }
  }
}
