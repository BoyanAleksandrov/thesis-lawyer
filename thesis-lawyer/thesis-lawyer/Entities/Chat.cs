using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using thesis_lawyer.Models;

public class Chat
{
    [Key] public int Id { get; set; }
    [JsonIgnore]
   public UserModel User { get; set; }
    public ICollection<HistoryChat> Messages { get; set; }
}