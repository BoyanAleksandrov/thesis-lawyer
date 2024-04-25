using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using thesis_lawyer.Models;

public class HistoryChat
{
    [Key]
    public int Id { get; set; }

    // Existing properties
    public string UserForeignKey { get; set; }
    public Chat Chat { get; set; }
    [JsonIgnore]
    public UserModel User { get; set; }
    public int? ChatId { get; set; }
    public string Message { get; set; }

    // New column
    public string MessageCategory { get; set; }
}
