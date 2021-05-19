using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Stock.EntityFrameWork.Model;

namespace Stock.EntityFrameWork
{
    public class StockDBContext : DbContext
    {
        public StockDBContext()
        {
        }

        public StockDBContext(DbContextOptions<StockDBContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
          => options.UseMySql("Server=81.71.143.71;Port=3306;Database=stockdb;Uid=StockDB;Pwd=StockDB@pwd;",
               ServerVersion.FromString("5.7.32-mysql"),
               mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend))
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OptionalPool>().HasKey(table => new {
                table.CategoryId,
                table.Code
            });
        }

        public virtual DbSet<StockEntity> StockEntity { get; set; }

        public virtual DbSet<BoardToStocks> BoardToStocks { get; set; }

        public virtual DbSet<Board> Board { get; set; }

        public virtual DbSet<StockComment> StockComment { get; set; }
        public virtual DbSet<CustomCategory> CustomCategory { get; set; }
        public virtual DbSet<OptionalPool> OptionalPool { get; set; }
        public virtual DbSet<AbuQuantModel> AbuQuantModel { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
