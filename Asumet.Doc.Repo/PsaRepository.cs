namespace Asumet.Doc.Repo
{
    using Asumet.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    internal class PsaRepository : RepositoryBase<Psa, int>, IPsaRepository
    {
        public PsaRepository(DocDbContext docDb)
            : base(docDb)
        {
        }

        protected override DbSet<Psa> DbSet => DocDb.Psas;

        public override Task<Psa?> GetByIdAsync(int id)
        {
            var result = DbSet
                .Include(psa => psa.Buyer)
                .Include(psa => psa.Supplier)
                .Include(psa => psa.PsaScraps)
                .FirstOrDefaultAsync(psa => psa.Id.Equals(id));

            return result;
        }
    }
}
