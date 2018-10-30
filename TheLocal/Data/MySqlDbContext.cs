using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using TheLocal.Models;

namespace TheLocal {
    public class MySqlDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public MySqlDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder {
                DataSource = "thelocaldata.cymuktbsfffe.us-east-2.rds.amazonaws.com",
                UserID = "root",
                Password = "rootroot",
                Pooling = false,
                InitialCatalog = "TheLocal"
            };

            optionsBuilder.UseSqlServer(sb.ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}
