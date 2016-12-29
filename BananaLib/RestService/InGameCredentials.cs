namespace BananaLib.RestService
{
    public class InGameCredentials
    {
        public string EncryptionKey { get; set; }

        public string HandshakeToken { get; set; }
        public bool InGame { get; set; }

        public string ServerIp { get; set; }

        public int? ServerPort { get; set; }

        public double? SummonerId { get; set; }
    }
}