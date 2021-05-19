using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.EntityFrameWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Serivices.AbuQuant;

namespace WebApplication.Controllers
{
    [Route("api/AbuQuant")]
    [ApiController]
    public class AbuQuantController : Controller
    {
        private readonly ILogger<AbuQuantController> _logger;
        private readonly StockDBContext _dbContext;
        private readonly IAbuQuantService _abuQuantService;


        public AbuQuantController(ILogger<AbuQuantController> logger, StockDBContext dbContext, IAbuQuantService abuQuantService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _abuQuantService = abuQuantService;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost]
        public async Task<IActionResult> GatherFinalScoreRank()
        {
            var list = await _abuQuantService.GetFinalScoreRank();
            foreach (var item in list)
            {
                _dbContext.AbuQuantModel.Add(item);
            }
            _dbContext.SaveChanges();
            return Ok();
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        public async Task<IActionResult> GetFinalScoreRank()

        {
            var createTime = _dbContext.AbuQuantModel.OrderByDescending(_ => _.CreateTime).FirstOrDefault().CreateTime;
            var dateTime = new DateTime(createTime.Year, createTime.Month, createTime.Day);
            var response = _dbContext.AbuQuantModel.Join(_dbContext.StockEntity, a => a.Code, s => s.Code, (_, __) => new
            {
                Code = _.Code,
                Type = __.Type,
                Rank = _.Rank,
                Score = _.Score,
                FW = _.FW,
                KC = _.KC,
                BS = _.BS,
                MC = _.MC,
                MP = _.MP,
                CreateTime = _.CreateTime,
            }).Join(_dbContext.StockComment, a => a.Code, s => s.Code, (_, __) => new
            {
                ShortCode= _.Type == 1 ? "sh" + _.Code : "sz" + _.Code,
                Symbol = _.Type+"."+ _.Code,
                Code = _.Code,
                Type = _.Type,
                Rank = _.Rank,
                Score = _.Score,
                FW = _.FW,
                KC = _.KC,
                BS = _.BS,
                MC = _.MC,
                MP = _.MP,
                CreateTime = _.CreateTime,
                SCreateTime = __.TDate,
                Name = __.Name,
                JGCYD = Convert.ToDouble(__.JGCYD),
                Ranking = Convert.ToDouble(__.Ranking),
                Focus = Convert.ToDouble(__.Focus),
                TotalScore = Convert.ToDouble(__.TotalScore),
                zlcb = Convert.ToDouble(__.ZLCB),
                zlcb20 = Convert.ToDouble(__.ZLCB20R),
                zlcb60 = Convert.ToDouble(__.ZLCB60R),
                IsRankingUp = Convert.ToDouble(__.RankingUp) > 0
            }).Where(_ => _.CreateTime > dateTime && _.SCreateTime == dateTime).OrderBy(_ => _.Rank).Take(100);

            return new JsonResult(response); ;
        }

        public string Code { get; set; }
        public int Rank { get; set; }
        public int Score { get; set; }
        public int FW { get; set; }
        public int KC { get; set; }
        public int BS { get; set; }
        public int MC { get; set; }
        public int MP { get; set; }
    }
}
