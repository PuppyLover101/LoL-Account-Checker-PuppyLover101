
using System;
using System.IO;

namespace BananaLib.LoLUtils
{
  public class LoLClientImages
  {
    private const string ICON_FILE = "assets/images/champions/{0}_Square_0.png";
    private const string PORTRAIT_FILE = "assets/images/champions/{0}_{1}.jpg";
    private const string SPLASH_FILE = "assets/images/champions/{0}_Splash_{1}.jpg";
    private const string SUMMONER_SPELL_FILE = "data/lolimg/summonerspells/{0}.png";
    private const string WARD_SKIN_FILE = "assets/storeImages/content/ward_skins/wardskin_{0}.jpg";
    private readonly string _clientDir;

    public LoLClientImages(string clientDir)
    {
      if (clientDir == null)
        throw new ArgumentNullException("clientDir");
      this._clientDir = clientDir;
    }

    private string GetImagePath(string fileType, string p1, int p2 = -1)
    {
      string path = Path.Combine(this._clientDir, p2 == -1 ? string.Format(fileType, (object) p1) : string.Format(fileType, (object) p1, (object) p2));
      if (!File.Exists(path))
        return (string) null;
      return path;
    }

    private string GetImagePath(string fileType, int p1)
    {
      string path = Path.Combine(this._clientDir, string.Format(fileType, (object) p1));
      if (!File.Exists(path))
        return (string) null;
      return path;
    }

    public string GetChampionIconPath(string championName)
    {
      return this.GetImagePath("assets/images/champions/{0}_Square_0.png", championName, -1);
    }

    public string GetChampionPortraitImagePath(string championName, int skinIndex = 0)
    {
      return this.GetImagePath("assets/images/champions/{0}_{1}.jpg", championName, skinIndex);
    }

    public string GetChampionSplashImagePath(string championName, int skinIndex = 0)
    {
      return this.GetImagePath("assets/images/champions/{0}_Splash_{1}.jpg", championName, skinIndex);
    }

    public string GetSummonerSpellIconPath(int spellId)
    {
      return this.GetImagePath("data/lolimg/summonerspells/{0}.png", spellId);
    }

    public string GetWardSkinImagePath(int wardId)
    {
      return this.GetImagePath("assets/storeImages/content/ward_skins/wardskin_{0}.jpg", wardId);
    }
  }
}
