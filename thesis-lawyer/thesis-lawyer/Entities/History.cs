using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using thesis_lawyer.Models;

namespace thesis_lawyer.Entities;

public class History
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string UserId { get; set; }

    [Required]
    public string Message { get; set; }

    public bool SentReceived { get; set; }
    [ForeignKey("UserId")]
    public ICollection<UserModel> UsersHistory { get; set; }
    
}