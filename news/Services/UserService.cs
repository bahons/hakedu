using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using news.DbModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace news.Services
{
    public interface IUserService
    {
        Task<User> GetById(string id);
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


        public async Task<User> GetById(string id)
        {
            var data = await _userManager.FindByIdAsync(id);
            return data;
        }


    }
}
