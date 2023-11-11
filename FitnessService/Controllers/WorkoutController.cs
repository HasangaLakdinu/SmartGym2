using FitnessService.Context;
using FitnessService.Models;
using FitnessService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FitnessService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : Controller
    {
        private FitnessContext _fitnessContext;
        private IUserService _userService;

        public WorkoutController(FitnessContext fitnessContext, IUserService userService)
        {
            _fitnessContext = fitnessContext;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<WorkOutdayInfo> Get()
        {
            Log.Information("workout detail retrieve request fired...");
            return _fitnessContext.WorkOutdayInfos;
        }

        [HttpPost]
        [Authorize]
        public void Post([FromBody] WorkoutDto value)
        {
            WorkOutdayInfo workoutInfo = new WorkOutdayInfo();
            workoutInfo.WorkoutHours = value.WorkoutHours;
            workoutInfo.WorkoutType = value.WorkoutType;    
            workoutInfo.Weight = value.Weight;
            workoutInfo.BurntCalories = value.BurntCalories;
            workoutInfo.UserName = _userService.GetMyName();
            workoutInfo.Date = DateTime.Now;
            _fitnessContext.WorkOutdayInfos.Add(workoutInfo);
            _fitnessContext.SaveChanges();
            Log.Information("workout created... {@value}", value);
        }
    }
}
