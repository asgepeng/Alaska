using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaska.Models
{
    public class PageRequestInfo
    {
        [JsonPropertyName("keyword")]
        public string Keyword { get; set; } = "";
        [JsonPropertyName("pageIndex")]
        public int PageIndex { get; set; } = 1;
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 100;
        [JsonPropertyName("sortColumnIndex")]
        public int SortColumnIndex { get; set; } = 0;
        [JsonPropertyName("sortOrder")]
        public int SortOrder { get; set; } = 0;
    }
    public class PageInfo
    {

    }
}
