namespace Asumet.Doc.Repo
{
    using Asumet.Entities;

    public class DocDbInitializer
    {
        public static void Initialize(DocDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Buyers.Any() || dbContext.Suppliers.Any() || dbContext.Psas.Any())
            {
                return;
            }

            var psas = PsaSeedData.GetSeedData();
            if (psas.Any())
            {
                foreach (var psa in psas)
                {
                    dbContext.Psas.Add(psa);
                }

                dbContext.SaveChanges();
            }
        }
    }
}