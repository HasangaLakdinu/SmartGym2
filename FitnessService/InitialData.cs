using FitnessService.Context;
using FitnessService.Models;

namespace FitnessService
{
    public static class InitialData
    {
        public static void Seed(this FitnessContext dbContext)
        {
            if (!dbContext.WorkOutdayInfos.Any())
            {
                dbContext.WorkOutdayInfos.Add(new WorkOutdayInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-1),
                    WorkoutType = "Cycling",
                    WorkoutHours = 1,
                    Weight = 71,
                    BurntCalories = 101
                });

                dbContext.WorkOutdayInfos.Add(new WorkOutdayInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-2),
                WorkoutType = "Swimming",
                    WorkoutHours = 2,
                    Weight = 72,
                    BurntCalories = 102
                });
                dbContext.WorkOutdayInfos.Add(new WorkOutdayInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-3),
                    WorkoutType = "Jogging",
                    WorkoutHours = 2,
                    Weight = 73,
                    BurntCalories = 103
                });
                dbContext.WorkOutdayInfos.Add(new WorkOutdayInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-4),
                    WorkoutType = "Cycling",
                    WorkoutHours = 2,
                    Weight = 70,
                    BurntCalories = 100
                });
                dbContext.WorkOutdayInfos.Add(new WorkOutdayInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-5),
                    WorkoutType = "Cycling",
                    WorkoutHours = 2,
                    Weight = 75,
                    BurntCalories = 105
                });

                dbContext.SaveChanges();
            }
            if (!dbContext.CheatMealInfos.Any())
            {
                dbContext.CheatMealInfos.Add(new CheatMealInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-1),
                    CheatMealName = "Pizza",
                    Weight = 0.2
                });
                dbContext.CheatMealInfos.Add(new CheatMealInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-2),
                    CheatMealName = "Burger",
                    Weight = 0.15
                });
                dbContext.CheatMealInfos.Add(new CheatMealInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-3),
                    CheatMealName = "Ice Cream",
                    Weight = 0.1
                });
                dbContext.CheatMealInfos.Add(new CheatMealInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-4),
                    CheatMealName = "Chocolate Cake",
                    Weight = 0.18
                });
                dbContext.CheatMealInfos.Add(new CheatMealInfo
                {
                    UserName = "Hasanga",
                    Date = DateTime.Now.AddDays(-5),
                    CheatMealName = "French Fries",
                    Weight = 0.12
                });
                dbContext.SaveChanges();
            }
        }
    }
}
