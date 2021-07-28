using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace JacobAssistant.Common.Models
{
    public class BotUser
    {
        [Key]
        public int Id { get; set; }
        
        public string UserId { get; set; }
        public string UserName { get; set; }
        
        [Comment("0: Telegram , 1: Wechat")]
        public int Type { get; set; }
        
    }
}