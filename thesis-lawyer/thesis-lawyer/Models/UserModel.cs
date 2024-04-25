using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using thesis_lawyer.Entities;

namespace thesis_lawyer.Models
{
    public class UserModel : IdentityUser
    {

    
        public string FirstName { get; set; }
        public bool isPremium { get; set; }
        [JsonIgnore]
        public ICollection<HistoryChat> HistoryChats { get; set; }
        [JsonIgnore]
        public ICollection<Chat> Chats { get; set; }
    }

    
}