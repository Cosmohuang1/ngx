using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApplication.SourceModel
{

    public class ResponseModel<T>
    {
        [JsonProperty("data")]
        public ResponseData<T> ResponseData { get; set; }
    }

    public class StockEntity
    {
        [JsonProperty("f14")]
        public string StockName { get; set; }

        [JsonProperty("f13")]
        public StockType StockType { get; set; }

        [JsonProperty("f12")]
        public string StockCode { get; set; }
    }

    public enum StockType
    {
        SZ,
        SH
    }
    public class ResponseData<T>
    {
        public int Total { get; set; }

        public List<T> Diff { get; set; }
    }


    public class IndustryBoard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BrokerSource BrokerSource { get; set; }
    }

    public enum BrokerSource
    {
        EASTMONEY,
        TENCENT
    }


    public enum BoardCategory
    {
        Region,
        Industry,
        Concept
    }

    public class BKEntity : BasicFieldMap
    {

    }

    public class BasicFieldMap
    {
        [JsonProperty("f12")]
        public string Id { get; set; }
        [JsonProperty("f14")]
        public string Name { get; set; }
    }
}
