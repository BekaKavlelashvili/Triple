using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Triple.Application.Shared
{
    public class QueryResultOfList<TListItem>
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("records")]
        public List<TListItem> Records { get; set; }
    }
}
