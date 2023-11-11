using LoginService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using AuthService.Services;
using AuthService.Models;
using LoginService.Context;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;

namespace LoginService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private AuthContext _authContext;

        public AuthController(IConfiguration configuration, IUserService userService, ITokenService tokenService, AuthContext authContext)
        {
            _configuration = configuration;
            _userService = userService;
            _tokenService = tokenService;
            _authContext = authContext;
        }


        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(AppUserDto request)
        {
            if (!_authContext.UsernameExists(request.Username))
            {
                _tokenService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                AppUser newUser = new AppUser();
                newUser.UserName = request.Username;
                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;

                _authContext.Users.Add(newUser);
                _authContext.SaveChanges();

                return Ok(newUser);
            }
            else
            {
                return BadRequest("Username already exists.");
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AppUserDto request)
        {
            if (_authContext.UsernameExists(request.Username))
            {
                AppUser user = _authContext.GetUserByUsername(request.Username);
                if (!_tokenService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }
                string token = _tokenService.BuildToken(_configuration, user.UserName,user.BranchId);

                var refreshToken = GenerateRefreshToken();
                SetRefreshToken(refreshToken, user);

                return Ok(token);
            }
            else
            {
                return BadRequest("User not found.");
            }
        }



        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            if (_authContext.UsernameExists(GetMe()))
            {
                AppUser user = _authContext.GetUserByUsername(GetMe());
                var refreshToken = Request.Cookies["refreshToken"];
                if (!user.RefreshToken.Equals(refreshToken))
                {
                    return Unauthorized("Invalid Refresh Token.");
                }
                else if (user.TokenExpires < DateTime.Now)
                {
                    return Unauthorized("Token expired.");
                }
                string token = _tokenService.BuildToken(_configuration, user.UserName, user.BranchId);
                var newRefreshToken = GenerateRefreshToken();
                SetRefreshToken(newRefreshToken, user);
                return Ok(token);
            }
            else {
                return Unauthorized("User not found.");
            }
        }

        public string GetMe()
        {
            var userName = _userService.GetMyName();
            return userName;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, AppUser user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            _authContext.SaveChanges();
        }

    }
}



