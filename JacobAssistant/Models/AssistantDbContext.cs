using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JacobAssistant.Models
{
    public partial class AssistantDbContext : DbContext
    {
        public AssistantDbContext()
        {
        }

        public AssistantDbContext(DbContextOptions<AssistantDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<ConfigType> ConfigTypes { get; set; }
        public virtual DbSet<Novel> Novels { get; set; }
        public virtual DbSet<NovelChapter> NovelChapters { get; set; }
        public virtual DbSet<NovelContent> NovelContents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=192.168.98.100;userid=aide;pwd=jacob_aide;port=3306;database=jacob_aide;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.FromString("8.0.22-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Config>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("config");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Value)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("value")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<ConfigType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("config_type");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("comment")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TypeName)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("type_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Novel>(entity =>
            {
                entity.ToTable("novel");

                entity.HasComment("小说表");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("author")
                    .HasComment("作者")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Brief)
                    .HasColumnType("varchar(2000)")
                    .HasColumnName("brief")
                    .HasComment("简介")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Cover)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("cover")
                    .HasComment("封面")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.DataSource)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("data_source")
                    .HasComment("来源网站")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.NovelName)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("novel_name")
                    .HasComment("小说名称")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RawId)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("raw_id")
                    .HasComment("来源网站的小说id")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RawUrl)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("raw_url")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");
            });

            modelBuilder.Entity<NovelChapter>(entity =>
            {
                entity.ToTable("novel_chapter");

                entity.HasIndex(e => e.ContentId, "novel_chapter_novel_content_id_fk");

                entity.HasIndex(e => e.NovelId, "novel_chapter_novel_id_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChapterName)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("chapter_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ContentId).HasColumnName("content_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.NovelId).HasColumnName("novel_id");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");

                entity.HasOne(d => d.Content)
                    .WithMany(p => p.NovelChapters)
                    .HasForeignKey(d => d.ContentId)
                    .HasConstraintName("novel_chapter_novel_content_id_fk");

                entity.HasOne(d => d.Novel)
                    .WithMany(p => p.NovelChapters)
                    .HasForeignKey(d => d.NovelId)
                    .HasConstraintName("novel_chapter_novel_id_fk");
            });

            modelBuilder.Entity<NovelContent>(entity =>
            {
                entity.ToTable("novel_content");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content")
                    .HasComment("处理好的纯文字内容")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.RawContent)
                    .HasColumnType("text")
                    .HasColumnName("raw_content")
                    .HasComment("未经处理的原始数据")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
