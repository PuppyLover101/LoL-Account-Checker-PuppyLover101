
using System;

namespace BananaLib.RestService
{
  public sealed class IpBannedException : Exception
  {
    public override string Message { get; }

    public IpBannedException()
    {
    }

    public IpBannedException(string message)
    {
      this.Message = message;
    }
  }
}
