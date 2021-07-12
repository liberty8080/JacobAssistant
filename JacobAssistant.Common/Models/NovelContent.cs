using System.Collections.Generic;

#nullable disable

namespace JacobAssistant.Common.Models
{
    public partial class NovelContent
    {
        public NovelContent()
        {
            NovelChapters = new HashSet<NovelChapter>();
        }

        public int Id { get; set; }
        public string RawContent { get; set; }
        public string Content { get; set; }

        public virtual ICollection<NovelChapter> NovelChapters { get; set; }
    }
}