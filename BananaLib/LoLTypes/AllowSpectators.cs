
using System.ComponentModel;

namespace BananaLib.LoLTypes
{
  public enum AllowSpectators
  {
    [Description("NONE")] None,
    [Description("ALL")] All,
    [Description("LOBBYONLY")] LobbyOnly,
    [Description("DROPINONLY")] DropInOnly,
  }
}
