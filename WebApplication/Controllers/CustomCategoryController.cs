using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.EntityFrameWork;
using Stock.EntityFrameWork.Model;
using System.Linq;

namespace WebApplication.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/tags")]
    [ApiController]
    public class CustomCategoryController : ControllerBase
    {
        private readonly ILogger<CustomCategoryController> _logger;
        private readonly StockDBContext _dbContext;

        public CustomCategoryController(ILogger<CustomCategoryController> logger, StockDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        [Authorize]
        public IActionResult GetTags()
        {
            var UserId = User.Claims.First(_ => _.Type == "userId").Value;
            var tags = _dbContext.CustomCategory.Where(_ => _.UserId == UserId && _.IsActived).Select(_ => new {Name= _.Name ,Id=_.Id}).ToList();
            return new JsonResult(tags);
        }

      
        [HttpPost]
        public IActionResult AddTags(string name)
        {
            var UserId = User.Claims.First(_ => _.Type == "userId").Value;
            var entity = new CustomCategory()
            {
                Name = name,
                UserId= UserId
            };
            _dbContext.CustomCategory.Add(entity);
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

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPut]
        public IActionResult UpdateTags(string name, int Id)
        {
            var UserId = User.Claims.First(_ => _.Type == "userId").Value;
            var entity = _dbContext.CustomCategory.FirstOrDefault(_ => _.UserId == UserId && _.Id == Id);
            entity.Name = name;
            _dbContext.CustomCategory.Update(entity);
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

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpDelete]
        public IActionResult DeleteTags(int Id)
        {
            var UserId = User.Claims.First(_ => _.Type == "userId").Value;
            var entity = _dbContext.CustomCategory.FirstOrDefault(_ => _.UserId == UserId && _.Id == Id);
            entity.IsActived = false;
            _dbContext.CustomCategory.Update(entity);
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
