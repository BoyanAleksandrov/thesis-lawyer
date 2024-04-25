using System.ComponentModel.DataAnnotations;
using thesis_lawyer.Models;

public class Chat
{
    [Key] public int Id { get; set; }
   
    public UserModel User { get; set; }
    public ICollection<HistoryChat> Messages { get; set; }
}