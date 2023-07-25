using Newtonsoft.Json;
using System;

namespace Triple.Shared
{
    public class SortAndPagingQuery
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public List<Filtering> Filters { get; set; } = new List<Filtering>();

        public List<Sorting> Sortings { get; set; } = new List<Sorting>();
    }

    public class Filtering
    {
        [JsonProperty("filterName")]
        public string FilterName { get; set; }

        [JsonProperty("filterValue")]
        public string FilterValue { get; set; }
    }

    public class Sorting
    {
        [JsonProperty("sortName")]
        public string SortName { get; set; }

        [JsonProperty("sortOrder")]
        public SortOrder SortOrder { get; set; }
    }

    public enum SortOrder
    {
        ASC,
        DESC
    }

    public class PeriodFilter
    {
        [JsonProperty("from")]
        public DateTime? From { get; set; }

        [JsonProperty("to")]
        public DateTime? To { get; set; }
    }
}
