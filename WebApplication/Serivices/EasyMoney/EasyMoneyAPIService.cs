using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication.SourceModel;

namespace WebApplication.Serivices.EasyMoney
{
    public class EasyMoneyAPIService : IEasyMoneyAPIService
    {
        private readonly HttpClient _httpClient; 

        public EasyMoneyAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task<IList<BKEntity>> GetIndustryBoard()
        {
            var responseMessage = await _httpClient.GetAsync("http://37.push2.eastmoney.com/api/qt/clist/get?pn=1&pz=2000&po=1&np=1&fltt=2&invt=2&fid=f3&fs=m:90+t:2+f:!50&fields=f12,f14");

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ResponseModel<BKEntity>>(responseContent);
            return model.ResponseData.Diff;
        }

        public async Task<IList<BKEntity>> GetRegionBoard()
        {
            var responseMessage = await _httpClient.GetAsync(" http://83.push2.eastmoney.com/api/qt/clist/get?pn=1&pz=31&po=1&np=1&fltt=2&invt=2&fid=f3&fs=m:90+t:1+f:!50&fields=f12,f14");

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ResponseModel<BKEntity>>(responseContent);
            return model.ResponseData.Diff;

        }

        public async Task<IList<string>> GetIndustryBoardMapStock(string cagegoryId)
        {
            var responseMessage = await _httpClient.GetAsync($"http://push2.eastmoney.com/api/qt/clist/get?po=1&pz=1000&pn=1&np=1&fltt=2&invt=2&fs=b:{cagegoryId}&fields=f12");

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject< ResponseModel<BasicFieldMap> > (responseContent);

            var bks = model.ResponseData.Diff;

            return bks.Select(_ => _.Id).ToList();
        }

        public async Task<IList<StockEntity>> GetStocks()
        {
            var responseMessage = _httpClient.GetAsync("http://2.push2.eastmoney.com/api/qt/clist/get?pn=1&pz=5000&po=1&np=1&ut=bd1d9ddb04089700cf9c27f6f7426281&fltt=2&invt=2&fid=f3&fs=m:0+t:6,m:0+t:13,m:0+t:80,m:1+t:2,m:1+t:23&fields=f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f12,f13,f14,f15,f16,f17,f18,f20,f21,f23,f24,f25,f22,f11,f62,f128,f136,f115,f152").GetAwaiter().GetResult();

            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<ResponseModel<StockEntity>>(responseContent);
            return model.ResponseData.Diff;
        }

        public async Task<IList<BKEntity>> GetConceptBoard()
        {
            var responseMessage = await _httpClient.GetAsync(" http://83.push2.eastmoney.com/api/qt/clist/get?pn=1&pz=500&po=1&np=1&fltt=2&invt=2&fid=f3&fs=m:90+t:3+f:!50&fields=f12,f14");

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ResponseModel<BKEntity>>(responseContent);
            return model.ResponseData.Diff;
        }

        public async Task<List<Stock.EntityFrameWork.Model.StockComment>> GetStockComment(string[] stockCodes)
        {
             var stockCode=string.Join(",",stockCodes);
            string url = $"http://dcfm.eastmoney.com/em_mutisvcexpandinterface/api/js/get?type=QGQP_LB&CMD={stockCode}&token=70f12f2f4f091e459a279469fe49eca5";
            var responseMessage = await _httpClient.GetAsync(url);

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
          
            //responseContent = responseContent.Replace('-', '0');
            return JsonConvert.DeserializeObject<Stock.EntityFrameWork.Model.StockComment[]>(responseContent).ToList();
           
        }
    }
}
