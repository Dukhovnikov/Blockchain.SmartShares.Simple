using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Blockchain.SmartShares
{
    [JsonObject]
    public class Hash
    {
        //[JsonProperty("hash")]
        public readonly byte[] hash; //{ get; }

        #region Constructors

        public Hash()
        {
        }

        public Hash(byte[] inputBytes)
        {
            hash = ComputeSha256(inputBytes);
        }

        public Hash(string inputString)
        {
            hash = ComputeSha256FromString(inputString);
        }
        #endregion 

        #region Methods
        private static byte[] ComputeSha256(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(bytes);
        }

        private static byte[] ComputeSha256FromString(string inputString)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(inputString);
                return sha256.ComputeHash(bytes);
            }
        }

        public override string ToString()
        {
            return HexConvert.FromBytes(hash);
        }

        #endregion  
        
        public class HashConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                
                throw new NotImplementedException();
            }

            public override bool CanConvert(Type objectType) => 
                objectType == typeof(byte[]);
        }
    }
}