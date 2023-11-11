using AuthService.Models;
using LoginService.Context;

namespace LoginService
{
    public static class InitialData
    {
        public static void Seed(this AuthContext dbContext) 
        {
            if (!dbContext.Users.Any()) 
            {
                //dbContext.Users.Add(new AppUser
                //{
                //    UserName = "Hasanga",
                //    Password = "123456",
                //});

                //dbContext.Users.Add(new AppUser
                //{
                //    UserName = "Lakdinu",
                //    Password = "Karunadasa",
                //});

                dbContext.SaveChanges();
            }
        }
    }
}
