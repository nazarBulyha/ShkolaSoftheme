using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MobileCommunication.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class LogMessage
    {
        [JsonProperty]
        public DateTime DateTime { get; set; } = DateTime.Now;

        [JsonProperty]
        public  string Message { get; set; } = "";

        [JsonProperty]
        public int Sender { get; set; } = 0;

        [JsonProperty]
        public int Receiver { get; set; } = 0;

        // not serialized because mode is opt-in
        public bool IsError { get; set; } = false;
    }
}
