using Stock.EntityFrameWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.SourceModel;

namespace WebApplication.Serivices.EasyMoney
{
   public  interface IEasyMoneyAPIService
    {
        public Task<IList<SourceModel.StockEntity>>GetStocks();
        public Task<IList<BKEntity>> GetIndustryBoard();
        public Task<IList<BKEntity>> GetRegionBoard();
        public Task<IList<BKEntity>> GetConceptBoard();
        public Task<IList<string>> GetIndustryBoardMapStock(string cagegoryId);

        public Task<List<StockComment>> GetStockComment(string[] stockCode);

    }
}
