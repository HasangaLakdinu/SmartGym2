using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginService.Context
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }

        public AppUser GetUserByUsername(string username)
        {
            return Users.FirstOrDefault(u => u.UserName == username);
        }
        public bool UsernameExists(string username)
        {
            return Users.Any(u => u.UserName == username);
        }
        //public void UpdateUserTokenInfo(AppUser updatedUser)
        //{
        //    var existingUser = Users.FirstOrDefault(u => u.UserName == updatedUser.UserName);

        //    if (existingUser != null)
        //    {
        //        // Update properties of existingUser with values from updatedUser
        //        existingUser.RefreshToken = updatedUser.RefreshToken;
        //        existingUser.TokenCreated = updatedUser.TokenCreated;
        //        existingUser.TokenCreated = updatedUser.TokenCreated;
        //        // ... and so on

        //        // Save changes to the database
        //        SaveChanges();
        //    }
        //}
    }
}
