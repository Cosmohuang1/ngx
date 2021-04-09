using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.EntityFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Serivices.EasyMoney;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EasyMoneyController : ControllerBase
    {
        private readonly ILogger<EasyMoneyController> _logger;
        private readonly StockDBContext _dbContext;
        private readonly IEasyMoneyAPIService _easyMoneyService;


        public EasyMoneyController(ILogger<EasyMoneyController> logger, StockDBContext dbContext, IEasyMoneyAPIService easyMoneyService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _easyMoneyService = easyMoneyService;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost]
        public async Task<IActionResult> GatherFinalScoreRank()
        {
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var list =  _dbContext.StockComment.Where(_=>_.TDate== date);
            return new JsonResult(list);
        }
    }
}
