
using RtmpSharp.IO;
using RtmpSharp.Messaging.Messages;
using System;

namespace BananaLib.RiotObjects.Exceptions
{
  [SerializedName("com.riotgames.platform.login.LoginFailedException")]
  [Serializable]
  public class LoginFailedException : ErrorMessage
  {
    [SerializedName("errorCode")]
    public string ErrorCode { get; set; }

    [SerializedName("message")]
    public string Message { get; set; }
  }
}
