using System;
using System.Collections.Generic;

#nullable disable

namespace JacobAssistant.Models
{
    public partial class EmailAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? State { get; set; }
        public int? Type { get; set; }
        public string SmtpServer { get; set; }
        public int? SmtpPort { get; set; }
    }
}
