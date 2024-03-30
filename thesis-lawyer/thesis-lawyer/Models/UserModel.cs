using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace thesis_lawyer.Models
{
    public class UserModel : IdentityUser
    {
  
        [Key]
        public string FirstName { get; set; }
        public bool isPremium { get; set; }
       
    }
}