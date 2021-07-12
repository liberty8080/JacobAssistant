using System;

#nullable disable

namespace JacobAssistant.Common.Models
{
    public partial class NovelChapter
    {
        public int Id { get; set; }
        public int? NovelId { get; set; }
        public string ChapterName { get; set; }
        public int? ContentId { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual NovelContent Content { get; set; }
        public virtual Novel Novel { get; set; }
    }
}