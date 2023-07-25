using Newtonsoft.Json;
using System;

namespace Triple.Shared.Results
{
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; }

        [JsonProperty("occuredDate")]
        public DateTime OccuredDate { get; }

        public Error(string message)
        {
            Message = message;
            OccuredDate = DateTime.UtcNow.AddHours(4);
        }
    }
}