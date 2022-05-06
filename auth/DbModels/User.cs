
namespace auth.DbModels
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
