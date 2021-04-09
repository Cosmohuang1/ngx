
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.EntityFrameWork;
using Stock.EntityFrameWork.Model;
using System.Linq;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionalPoolController : ControllerBase
    {
        private readonly ILogger<OptionalPoolController> _logger;
        private readonly StockDBContext _dbContext;

        public OptionalPoolController(ILogger<OptionalPoolController> logger, StockDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetOptionalPoolByCategory(int category)
        {
            var optionalCodes = _dbContext.OptionalPool.Where(_ => _.CategoryId == category).Select(_ =>new { _.Code ,_.CreateDateTime}).ToList();
            return new JsonResult(optionalCodes);
        }

        [HttpPost]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult AddOptionalPoolWithCodes(int category,string[] codes)
        {
            var list= _dbContext.OptionalPool.Where(_ => _.CategoryId == category).Select(_ => _.Code).ToList();
            
            foreach (var code in codes)
            {
                if (list.Contains(code))
                    continue;

                var entity = new OptionalPool()
                {
                    CategoryId = category,
                    Code = code
                };
                _dbContext.OptionalPool.Add(entity);
            }           
            var flag = _dbContext.SaveChanges();
            if (flag > 0)
            {
                return new JsonResult("ok");
            }
            else
            {
                return new JsonResult("fail");
            }
        }

        [HttpDelete]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult OptionalCodeTags(int category, string[] codes)
        {
            foreach (var code in codes)
            {
                var entity = new OptionalPool()
                {
                    CategoryId = category,
                    Code = code
                };
                _dbContext.OptionalPool.Remove(entity);
            }
            var flag = _dbContext.SaveChanges();
            if (flag > 0)
            {
                return Ok();
            }
            else
            {
                return new JsonResult("fail");
            }
        }
    }
}
