using System.ComponentModel.DataAnnotations;

namespace thesis_lawyer.Models;

public class UpdateUserModel
{
   
    [EmailAddress] 
    public string Email { get; set; }

    
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    
    public string Password { get; set; }

    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string NormalizedEmail { get; set; }

    public string FirstName { get; set; }
    
    public bool IsPremium { get; set; }
}