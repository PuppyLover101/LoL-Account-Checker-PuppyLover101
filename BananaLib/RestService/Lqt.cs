
using Newtonsoft.Json;
using System;

namespace BananaLib.RestService
{
    public class Lqt
    {
        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("other")]
        public string Other { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("partner_token")]
        public string PartnerToken { get; set; }

        [JsonProperty("resources")]
        public string Resources { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject((object)this, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}