using Microsoft.AspNetCore.Identity;

namespace news.DbModels
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
