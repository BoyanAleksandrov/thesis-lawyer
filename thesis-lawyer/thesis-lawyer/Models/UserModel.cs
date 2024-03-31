using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thesis_lawyer.Models
{
    public class UserModel : IdentityUser
    {

    
        public string FirstName { get; set; }
        public bool isPremium { get; set; }
       
    }
}