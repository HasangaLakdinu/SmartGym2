using FitnessService.Context;
using FitnessService.Models;
using FitnessService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FitnessService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheatMealController : Controller
    {
        private FitnessContext _fitnessContext;
        private IUserService _userService;

        public CheatMealController(FitnessContext fitnessContext, IUserService userService)
        {
            _fitnessContext = fitnessContext;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<CheatMealInfo> Get()
        {
            Log.Information("cheat meal retrieve request fired...");
            return _fitnessContext.CheatMealInfos;
        }

        [HttpPost]
        [Authorize]
        public void Post([FromBody] CheatMealDto value)
        {
            CheatMealInfo info = new CheatMealInfo();
            info.Weight = value.Weight;
            info.CheatMealName = value.CheatMealName;
            info.UserName = _userService.GetMyName();
            info.Date = DateTime.Now;
            _fitnessContext.CheatMealInfos.Add(info);
            _fitnessContext.SaveChanges();
            Log.Information("cheat meal created... {@value}",value);
        }
    }
}
