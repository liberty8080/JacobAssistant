using System;
using System.ComponentModel.DataAnnotations;

namespace JacobAssistant.Common.Models
{
    public class EmailMessage
    {
        [Key] 
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Sender { get; set; }

        public DateTimeOffset Date { get; set; }

        // public string 
        public string Content { get; set; }
        public string Priority { get; set; }
        public string To { get; set;   }
    }
}