
// Type: BananaLib.RegionInfo



namespace BananaLib
{
  public static class RegionInfo
  {
    private static int _gameServerPort = 2099;

    public static string FullName(this Region region)
    {
      return region.GetAttribute<FullNameAttribute>().Value;
    }

    public static string LoginQueueServer(this Region region)
    {
      return region.GetAttributes<LoginQueueAttribute>()[0].Value;
    }

    public static string GameServerAddress(this Region region)
    {
      return region.GetAttributes<GameServerAddressAttribute>()[0].Value;
    }

    public static int GameServerPort(this Region region)
    {
      return RegionInfo._gameServerPort;
    }

    public static string GameServer(this Region region)
    {
      return string.Format("rtmps://{0}:{1}", (object) region.GameServerAddress(), (object) RegionInfo._gameServerPort);
    }

    public static string Locale(this Region region)
    {
      return region.GetAttributes<LocaleAttribute>()[0].Value;
    }

    public static bool UseGarena(this Region region)
    {
      return region.GetAttributes<UseGarenaAttribute>()[0].Value;
    }
  }
}
