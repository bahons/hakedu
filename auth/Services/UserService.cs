using auth.DbModels;
using auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace auth.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<User> GetById(string id);

        Task<Boolean> Register(AuthenticateRequest model);
    }

    public class UserService : IUserService
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, UserManager<User> userM, SignInManager<User> signInManager)
        {
            _appSettings = appSettings.Value;
            _userManager = userM;
            _signInManager = signInManager;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                // authentication successful so generate jwt token
                var token = generateJwtToken(user);

                return new AuthenticateResponse(user, token);
            }
            return null;
            
        }

        public async Task<Boolean> Register(AuthenticateRequest authenticateRequest)
        {
            User user = new User { Email = authenticateRequest.Username, UserName = authenticateRequest.Username };
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, authenticateRequest.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<User> GetById(string id)
        {
            var data = await _userManager.FindByIdAsync(id);
            return data;
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
