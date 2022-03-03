using CoreProject_DataAccess.FluentConfig;
using CoreProject_Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreProject_DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{
        //}

        public DbSet<BookDetailsFromView> BookDetailsFromViews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Fluent_BookDetail> Fluent_BookDetails { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //category tablename and columnname
            modelBuilder.Entity<Category>().ToTable("tbl_Category");
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("CategoryName");

            //compositekey
            modelBuilder.Entity<BookAuthor>().HasKey(x => new { x.Author_Id, x.Book_Id });

            //one to one relation b/w book and bookdetail
            modelBuilder.Entity<Fluent_Book>().HasOne(x => x.Fluent_BookDetail).WithOne(x => x.Fluent_Book).HasForeignKey<Fluent_Book>("BookDetail_Id");

            //one to many relation b/w book and publisher
            modelBuilder.Entity<Fluent_Book>().HasOne(x => x.Fluent_Publisher).WithMany(x => x.Fluent_Book).HasForeignKey(x => x.Publisher_Id);

            //many to many relation
            modelBuilder.Entity<Fluent_BookAuthor>().HasKey(x => new { x.Author_Id, x.Book_Id });
            modelBuilder.Entity<Fluent_BookAuthor>().HasOne(x=>x.Fluent_Book).WithMany(x=>x.Fluent_BookAuthors).HasForeignKey(x=>x.Book_Id);
            modelBuilder.Entity<Fluent_BookAuthor>().HasOne(x=>x.Fluent_Author).WithMany(x=>x.Fluent_BookAuthors).HasForeignKey(x=>x.Author_Id);


            //configurations
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDetailConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());

            modelBuilder.Entity<BookDetailsFromView>().HasNoKey().ToView("GetAllBookDetails");
        }
    }
}
