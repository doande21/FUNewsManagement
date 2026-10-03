using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection.Emit;

namespace BusinessObjects
{
    public class FUNewsManagementContext : DbContext
    {
        public FUNewsManagementContext() { }
        public FUNewsManagementContext(DbContextOptions<FUNewsManagementContext> options) : base(options) { }

        public virtual DbSet<SystemAccount> SystemAccounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<NewsArticle> NewsArticles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsArticle>()
                .HasMany(n => n.Tags)
                .WithMany(t => t.NewsArticles)
                .UsingEntity<Dictionary<string, object>>(
                    "NewsTag",
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagID"),
                    j => j.HasOne<NewsArticle>().WithMany().HasForeignKey("NewsArticleID")
                );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Lấy chuỗi kết nối từ MVC App
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}