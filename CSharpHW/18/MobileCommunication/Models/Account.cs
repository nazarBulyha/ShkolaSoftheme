using Newtonsoft.Json;
using System;

namespace MobileCommunication.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class Account
    {
        public Account(int number)
        {
            Number = number;
        }

        [JsonProperty]
        public int Number { get; private set; }

        [JsonProperty]
        public string Name { get; set; } = "Ivan";

        [JsonProperty]
        public string Surname { get; set; } = "Ivanovych";

        [JsonProperty]
        public string Email { get; set; } = "";

        [JsonProperty]
        public DateTime DateBirth { get; set; } = DateTime.Now;

        [JsonProperty]
        public AddressBook AddressBook { get; set; } = new AddressBook();
    }
}
