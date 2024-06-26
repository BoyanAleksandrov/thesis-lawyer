﻿using System.ComponentModel.DataAnnotations;

namespace thesis_lawyer.Models;

public class CreateUserModel
{
    [Required] 
    [EmailAddress] 
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    
    public string Password { get; set; }
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public bool IsPremium { get; set; }
}