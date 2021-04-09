using HtmlAgilityPack;
using Stock.EntityFrameWork.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplication.Serivices.AbuQuant
{
    public class AbuQuantService : IAbuQuantService
    {
        private readonly HttpClient _httpClient;
        private readonly string _defaultAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36";
        public AbuQuantService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<IList<AbuQuantModel>> GetFinalScoreRank()
        {
            List<AbuQuantModel> abuQuantModels = new List<AbuQuantModel>();
            for (int pageIndex = 1; pageIndex <= 46; pageIndex++)
            {
                var html = await GetRank(pageIndex);
                abuQuantModels.AddRange( await GetFinalScoreRankByPage(html));
            }
            return abuQuantModels;
        }

        public async Task<IList<AbuQuantModel>> GetFinalScoreRankByPage(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);//html字符串
            List<AbuQuantModel> abuQuantModels = new List<AbuQuantModel>();
            var stockItem = doc.DocumentNode.SelectNodes(@"//ul[@class='newsList']/li");
            foreach (var item in stockItem)
            {
                var div = item.ChildNodes.Where(_ => _.Name.ToLower() == "div");
                var title = div.FirstOrDefault();
                var stockCodeText = title.Descendants().FirstOrDefault(_ => _.Name.ToLower() == "div").InnerText;
                var stockCode = new Regex(@"\d+").Match(stockCodeText).Value;

                var bodies = div.LastOrDefault().Descendants().FirstOrDefault(_ => _.Name.ToLower() == "div").OuterHtml;
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
                        kc = link.InnerText.Replace("\r\n", "").Trim();
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

        public async  Task<string> GetRank(int page = 1)
        {
            _httpClient.DefaultRequestHeaders.Add("user-agent", _defaultAgent);
            string url = $"https://www.abuquant.com/rankDetail/final_score_rank/cn/day/{page}#selectExchange";
            var responseMessage = await _httpClient.GetAsync(url);
            return await responseMessage.Content.ReadAsStringAsync();   
        }
    }
}
