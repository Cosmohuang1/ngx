using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.EntityFrameWork;
using Stock.EntityFrameWork.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Serivices.EasyMoney;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDBController : ControllerBase
    {
        private readonly ILogger<InitialDBController> _logger;
        private readonly StockDBContext _dbContext;
        private readonly IEasyMoneyAPIService _easyMoneyAPIService;

        public InitialDBController(ILogger<InitialDBController> logger, StockDBContext dbContext, IEasyMoneyAPIService easyMoneyAPIService)
        { 
            _logger=logger;
            _dbContext = dbContext;
            _easyMoneyAPIService = easyMoneyAPIService;

        }

        [HttpGet]
        [Route("Init1StockData")]
        public async Task<IActionResult> StockData()
        {
           var stocks= await _easyMoneyAPIService.GetStocks();
            foreach(var stock in stocks)
            {              
                _dbContext.StockEntity.Add(new StockEntity() { Code = stock.StockCode, Name = stock.StockName ,Type=(int)stock.StockType}); 
            }
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("Init2Board")]
        public async Task<IActionResult> IndustryBoard()
        {
            var bks = await _easyMoneyAPIService.GetIndustryBoard();
            foreach (var bk in bks)
            {
                var entity = _dbContext.Board.FirstOrDefault(_ => _.CagegoryId.Equals(bk.Id));
                if (entity != null)
                    continue;
                _dbContext.Board.Add(new Board() { CagegoryId = bk.Id,Name=bk.Name,BrokerSource=BrokerSource.EASTMONEY,BoardCategory=BoardCategory.Industry }); 
            }
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("Init3BoardToStock")]
        public async Task<IActionResult> BoardToStock()
        {
            var bks = await _easyMoneyAPIService.GetIndustryBoard();
            foreach (var bk in bks)
            {
                var stocks = await _easyMoneyAPIService.GetIndustryBoardMapStock(bk.Id);
                foreach (var item in stocks)
                {
                    _dbContext.BoardToStocks.Add(new BoardToStocks() { CagegoryId = bk.Id, StockCode = item });
                }
            }
            _dbContext.SaveChanges();

            return Ok();
        }


        [HttpGet]
        [Route("Init4RegionBoard")]
        public async Task<IActionResult> RegionBoard()
        {
            var bks = await _easyMoneyAPIService.GetRegionBoard();
            foreach (var bk in bks)
            {
                var entity = _dbContext.Board.FirstOrDefault(_ => _.CagegoryId.Equals(bk.Id));
                if (entity != null)
                    continue;
                _dbContext.Board.Add(new Board() { CagegoryId = bk.Id, Name = bk.Name, BrokerSource = BrokerSource.EASTMONEY, BoardCategory = BoardCategory.Region });

                var stocks = await _easyMoneyAPIService.GetIndustryBoardMapStock(bk.Id);
                foreach (var item in stocks)
                {
                    _dbContext.BoardToStocks.Add(new BoardToStocks() { CagegoryId = bk.Id, StockCode = item });
                }
            }
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("Init5ConceptBoard")]
        public async Task<IActionResult> ConceptBoard()
        {
            var bks = await _easyMoneyAPIService.GetConceptBoard();
            foreach (var bk in bks)
            {
                var entity = _dbContext.Board.FirstOrDefault(_ => _.CagegoryId.Equals(bk.Id));
                if (entity != null)
                    continue;
                _dbContext.Board.Add(new Board() { CagegoryId = bk.Id, Name = bk.Name, BrokerSource = BrokerSource.EASTMONEY, BoardCategory = BoardCategory.Concept });

                var stocks = await _easyMoneyAPIService.GetIndustryBoardMapStock(bk.Id);
                foreach (var item in stocks)
                {
                    _dbContext.BoardToStocks.Add(new BoardToStocks() { CagegoryId = bk.Id, StockCode = item });
                }
            }
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("InitStockComment")]
        public async Task<IActionResult> StockComment()
        {
          var list=  await GetStockCommentList(0);
            foreach(var item in list)
            {
                _dbContext.StockComment.Add(item);
            }
            _dbContext.SaveChanges();
            return Ok();
        }

        private async  Task<List<StockComment>>  GetStockCommentList(int type)
        {
            var list = new List<StockComment>();

            var stocks =  _dbContext.StockEntity.Where(_ => _.Type == type && !_.Name.Contains("退")&&!_.Name.StartsWith("PT")).Select(_=>_.Code).ToList();
            var page= stocks.Count/100;
            for(int i = 0; i <= page; i++)
            {
                var s= stocks.Skip(i * 100).Take(100).ToArray();
                var stockComment = await _easyMoneyAPIService.GetStockComment(s);
                list.AddRange(stockComment);
            }           
            return list;
        }

        [HttpGet]
        [Route("InitStockComment1")]
        public async Task<IActionResult> StockComment1()
        {
            var list = await GetStockCommentList(1);
            foreach (var item in list)
            {
                _dbContext.StockComment.Add(item);
            }
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}
