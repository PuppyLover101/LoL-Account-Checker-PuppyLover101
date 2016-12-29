using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaLib.RestService
{
    public class GasToken
    {
        [JsonProperty("date_time")]
        public long DateTime { get; set; }

        [JsonProperty("gas_account_id")]
        public long GasAccountId { get; set; }

        [JsonProperty("pvpnet_account_id")]
        public long PvpnetAccountId { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("summoner_name")]
        public string SummonerName { get; set; }

        [JsonProperty("vouching_key_id")]
        public string VouchingKeyId { get; set; }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject((object)this, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
