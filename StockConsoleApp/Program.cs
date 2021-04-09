using HtmlAgilityPack;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace StockConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var AbuQuant = GetFinalRank();
            foreach (var item in AbuQuant)
            {
                Console.WriteLine($"{item.Code}\t{item.Rank}\t{item.Score}\t{item.KC}\t{item.MP}\t{item.MC}\t{item.FW}");
            }

            Console.WriteLine("获取所有股票");

            HttpClient httpClient = new HttpClient();

            var responseMessage = httpClient.GetAsync("http://2.push2.eastmoney.com/api/qt/clist/get?pn=1&pz=5000&po=1&np=1&ut=bd1d9ddb04089700cf9c27f6f7426281&fltt=2&invt=2&fid=f3&fs=m:0+t:6,m:0+t:13,m:0+t:80,m:1+t:2,m:1+t:23&fields=f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f12,f13,f14,f15,f16,f17,f18,f20,f21,f23,f24,f25,f22,f11,f62,f128,f136,f115,f152").GetAwaiter().GetResult();

            var responseContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var model = JsonConvert.DeserializeObject<ResponseModel<StockEntity>>(responseContent);
            var stocks = model.ResponseData.Diff;
            Console.WriteLine("股票代码\t股票名称\t\n");
            foreach (var stock in stocks)
            {
                Console.WriteLine($"{stock.StockCode}\t{stock.StockName}\t");
            }



            Console.WriteLine("获取地域板块");
            var provinces = GetProvinces();

            foreach (var province in provinces)
            {
                province.Stocks = GetStockFromProvince(province.Name);
            }

            var count = 0;
            foreach (var province in provinces)
            {
                Console.WriteLine("地域板块\t 股票数量\t");
                count += province.Stocks.Count;
                Console.WriteLine(province.Name + "\t" + province.Stocks.Count + "\t");
            }
            Console.WriteLine("地域总数" + count);

            var bks = GetIndustryBoards();

            foreach (var bk in bks)
            {
                Console.WriteLine($"\t ====={bk.Name}板块====");
                foreach (var stock in bk.Stocks)
                {
                    var entity = stocks.FirstOrDefault(_ => _.StockCode == stock);
                    if (entity == null)
                    {
                        continue;
                    }
                    Console.WriteLine("\t" + entity.StockCode + ":" + entity.StockName);
                }
            }

        }

        public static string GetStatsHtml
        {
            get
            {
                try
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    string url = "http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2020/index.html";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    Stream stream;
                    StreamReader sr;

                    if (response.ContentEncoding == "gzip")
                    {
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                        sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
                    }
                    else
                    {
                        stream = response.GetResponseStream();
                        sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
                    }

                    string strHtml = sr.ReadToEnd();

                    stream.Close();
                    sr.Close();
                    return strHtml;

                }
                catch (WebException e)
                {
                    e.StackTrace.ToString();
                    return string.Empty;
                }
            }

        }

        public static List<Province> GetProvinces()
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(GetStatsHtml);//html字符串

            var provinces = doc.DocumentNode.SelectNodes(@"//tr[@class='provincetr']");
            List<Province> list = new List<Province>();
            foreach (var province in provinces)
            {
                var nodes = province.ChildNodes;
                foreach (var n in nodes)
                {
                    if (n.FirstChild.Name != "a")
                    {
                        continue;
                    }
                    var provinceId = Convert.ToInt32(n.FirstChild.Attributes[0].Value.Replace(".html", ""));
                    var provinceName = n.InnerText.Replace("省", "").Replace("市", "").Replace("自治区", "");
                    if (provinceName.Length > 3)
                    {
                        provinceName = provinceName.Substring(0, 2);
                    }
                    list.Add(new Province() { Id = provinceId, Name = provinceName });
                }
            }

            return list;

        }

        public static List<AbuQuantModel> GetFinalRank()
        {
            var doc = new HtmlDocument();
            var html =  GetRank();
            doc.LoadHtml(html);//html字符串
            List<AbuQuantModel> abuQuantModels = new List<AbuQuantModel>();
            var stockItem = doc.DocumentNode.SelectNodes(@"//ul[@class='newsList']/li");
            foreach (var item in stockItem)
            {
               var div=  item.ChildNodes.Where(_=>  _.Name.ToLower() == "div");
               var title=  div.FirstOrDefault();
                var stockCodeText = title.Descendants().FirstOrDefault(_ => _.Name.ToLower() == "div").InnerText;                
                var stockCode = new Regex(@"\d+").Match(stockCodeText).Value;

                var bodies=  div.LastOrDefault().Descendants().FirstOrDefault(_ => _.Name.ToLower() == "div").OuterHtml;           
                var doc1 = new HtmlDocument();
             
                doc1.LoadHtml(bodies);
                var node = doc1.DocumentNode;
                var rank = new Regex(@"\d+").Match(node.SelectSingleNode(@"//div[@class='layui-col-xs8']//p[1]").InnerText).Value;
                var score = new Regex(@"\d+").Match(node.SelectSingleNode(@"//div[@class='layui-col-xs8']//p[2]").InnerText).Value;
                var updateDate = new Regex(@"\d+-\d+\s\d+:\d+").Match(item.SelectSingleNode(@"//div[@class='layui-col-xs8']//p[5]").InnerText).Value;

                var rowNodes = node.SelectNodes("div[@class='layui-row']//div[@class='layui-row']").Where(_ => _.Name.ToLower() == "div" && _.Attributes.Count == 1).ToArray();
                string fw = "0", kc = "0", mc = "0", bs = "0", mp = "0";
                foreach (var row in rowNodes)
                {
                    var link = row.Descendants().Where(_ => _.Name.ToLower() == "a").ToArray()[1];
                    if (link.OuterHtml.Contains("#fw")) 
                    {
                        fw = link.InnerText.Replace("\r\n", "").Trim();
                        continue;
                    }
                    if (link.OuterHtml.Contains("#kc"))
                    {
                        kc= link.InnerText.Replace("\r\n", "").Trim();
                        continue;
                    }
                    if (link.OuterHtml.Contains("#mc"))
                    {
                        mc = link.InnerText.Replace("\r\n", "").Trim();
                        continue;
                    }
                    if (link.OuterHtml.Contains("#bs"))
                    {
                        bs = link.InnerText.Replace("\r\n", "").Trim();
                        continue;
                    }
                    if (link.OuterHtml.Contains("#mp"))
                    {
                        mp = link.InnerText.Replace("\r\n", "").Trim();
                        continue;
                    }
                }
                AbuQuantModel model = new AbuQuantModel()
                {
                    Code = stockCode,
                    Rank = Convert.ToInt32(rank),
                    Score = Convert.ToInt32(score),
                    CreateTime = Convert.ToDateTime(DateTime.Now.Year.ToString() + " " + updateDate),
                    FW = Convert.ToInt32(fw),
                    KC = Convert.ToInt32(kc),
                    MC = Convert.ToInt32(mc),
                    BS = Convert.ToInt32(bs),
                    MP = Convert.ToInt32(mp)
                };
                abuQuantModels.Add(model);
            }
            return abuQuantModels;

        }

        public static string GetRank(int page = 1)
        {

            try
            {
                string url = $"https://www.abuquant.com/rankDetail/final_score_rank/cn/day/{page}#selectExchange";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                var header = new NameValueCollection();
                header.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");
                request.Headers.Add(header);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream stream;
                StreamReader sr;

                if (response.ContentEncoding == "gzip")
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
                }
                else
                {
                    stream = response.GetResponseStream();
                    sr = new StreamReader(stream, Encoding.UTF8);
                }

                string strHtml = sr.ReadToEnd();

                stream.Close();
                sr.Close();
                return strHtml;

            }
            catch (WebException e)
            {
                e.StackTrace.ToString();
                return string.Empty;
            }

        }

        public static List<IndustryBoard> GetIndustryBoards()
        {
            List<IndustryBoard> list = new List<IndustryBoard>();

            HttpClient httpClient = new HttpClient();

            //http://quote.eastmoney.com/center/boardlist.html#industry_board

            var responseMessage = httpClient.GetAsync("http://37.push2.eastmoney.com/api/qt/clist/get?pn=1&pz=2000&po=1&np=1&ut=bd1d9ddb04089700cf9c27f6f7426281&fltt=2&invt=2&fid=f3&fs=m:90+t:2+f:!50&fields=f12,f14").GetAwaiter().GetResult();

            var responseContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var model = JsonConvert.DeserializeObject<ResponseModel<BKEntity>>(responseContent);
            var bks = model.ResponseData.Diff;
            foreach (var bk in bks)
            {
                IndustryBoard industryBoard = new IndustryBoard();
                Console.WriteLine("板块编号\t板块名称\t\n");
                Console.WriteLine($"{bk.Id}\t{bk.Name}\t");
                industryBoard.Id = bk.Id;
                industryBoard.Name = bk.Name;
                industryBoard.BrokerSource = BrokerSource.EASTMONEY;

                industryBoard.Stocks = GetStockFromIndustryBoard(bk.Id);
                list.Add(industryBoard);
            }
            return list;
        }

        public static IList<string> GetStockFromIndustryBoard(string boardId)
        {
            HttpClient httpClient = new HttpClient();
            var responseMessage = httpClient.GetAsync($"http://push2.eastmoney.com/api/qt/clist/get?po=1&pz=1000&pn=1&np=1&fltt=2&invt=2&fs=b:{boardId}&fields=f12").GetAwaiter().GetResult();

            var responseContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var model = JsonConvert.DeserializeObject<ResponseModel<StockEntity>>(responseContent);

            var bks = model.ResponseData.Diff;
            return bks.Select(_ => _.StockCode).ToList();

        }

        public static IList<string> GetStockFromProvince(string name)
        {
            HttpClient httpClient = new HttpClient();
            string url = "http://55.push2.eastmoney.com/api/qt/clist/get?pn=1&pz=40&po=1&np=1&fltt=2&invt=2&fid=f3&fs=m:90+t:1+f:!50&fields=f12,f14";
            var responseMessage = httpClient.GetAsync(url).GetAwaiter().GetResult();

            var responseContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var model = JsonConvert.DeserializeObject<ResponseModel<BasicFieldMap>>(responseContent);

            var bks = model.ResponseData.Diff;
            var item = bks.FirstOrDefault(_ => _.Name.Replace("板块", "").Equals(name));

            httpClient = new HttpClient();
            string provinceUrl = $"http://push2.eastmoney.com/api/qt/clist/get?fid=f62&po=1&pz=1000&pn=1&np=1&fs=b%3A{item.Id}&fields=f12,f14";

            responseMessage = httpClient.GetAsync(provinceUrl).GetAwaiter().GetResult();

            responseContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var model1 = JsonConvert.DeserializeObject<ResponseModel<BasicFieldMap>>(responseContent);
            var basicFields = model1.ResponseData.Diff;

            return basicFields.Select(_ => _.Id).ToList();

        }

    }


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
        public string StockType { get; set; }

        [JsonProperty("f12")]
        public string StockCode { get; set; }
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

    public class ResponseData<T>
    {
        public int Total { get; set; }

        public List<T> Diff { get; set; }
    }

    public class Province
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<string> Stocks { get; set; }
    }

    public class IndustryBoard
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public BrokerSource BrokerSource { get; set; }

        public IList<string> Stocks { get; set; }
    }

    public enum BrokerSource
    {
        EASTMONEY,
        TENCENT
    }

    public class AbuQuantModel
    {
        public string Code { get; set; }
        public int Rank { get; set; }
        public int Score { get; set; }
        public int FW { get; set; }
        public int KC { get; set; }
        public int BS { get; set; }
        public int MC { get; set; }
        public int MP { get; set; }
        public DateTime CreateTime { get; set; }
    }

}
