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

        public override async Task<Psa?> GetByIdAsync(int id)
        {
            var result = await DbSet
                .Include(psa => psa.Buyer)
                .Include(psa => psa.Supplier)
                .Include(psa => psa.PsaScraps)
                .FirstOrDefaultAsync(psa => psa.Id.Equals(id));

            return result;
        }

        public override async Task<Psa?> InsertEntityAsync(Psa entity)
        {
            if (entity.Buyer.Id > 0)
            {
                entity.Buyer = await DocDb.Buyers.FirstOrDefaultAsync(b => b.Id == entity.Buyer.Id);
            }
            
            if (entity.Supplier.Id > 0)
            {
                entity.Supplier = await DocDb.Suppliers.FirstOrDefaultAsync(s => s.Id == entity.Supplier.Id);
            }
            
            var result = await base.InsertEntityAsync(entity);
            return result;
        }
    }
}
