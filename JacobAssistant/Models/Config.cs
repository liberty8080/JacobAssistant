using System;
using System.Collections.Generic;

#nullable disable

namespace JacobAssistant.Models
{
    public partial class Config
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? Type { get; set; }
    }
}
