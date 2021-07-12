using System;
using System.Collections.Generic;

#nullable disable

namespace JacobAssistant.Common.Models
{
    public partial class Novel
    {
        public Novel()
        {
            NovelChapters = new HashSet<NovelChapter>();
        }

        public int Id { get; set; }
        public string RawId { get; set; }
        public string NovelName { get; set; }
        public string DataSource { get; set; }
        public string Brief { get; set; }
        public string Cover { get; set; }
        public string Author { get; set; }
        public string RawUrl { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<NovelChapter> NovelChapters { get; set; }
    }
}
