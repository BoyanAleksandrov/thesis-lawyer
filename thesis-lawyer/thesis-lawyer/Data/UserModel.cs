using Microsoft.AspNetCore.Identity;

namespace thesis_lawyer.Data;

public class UserModel : IdentityUser
{
    public string FirstName { get; set; }
    public bool isPremium { get; set; }
}