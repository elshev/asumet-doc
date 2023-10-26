namespace Asumet.Doc.Repo
{
    using Asumet.Models;
    using Microsoft.EntityFrameworkCore;

    public class DocDbContext : DbContext
    {
        public DocDbContext(DbContextOptions<DocDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseIdentityColumns();
        }

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<PsaScrap> PsaScraps { get; set; }

        public DbSet<Psa> Psas { get; set; }
    }
}
