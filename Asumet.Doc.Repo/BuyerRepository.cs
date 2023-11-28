namespace Asumet.Doc.Repo
{
    using Asumet.Entities;
    using Microsoft.EntityFrameworkCore;

    internal class BuyerRepository : RepositoryBase<Buyer, int>, IBuyerRepository
    {
        public BuyerRepository(DocDbContext docDb)
            : base(docDb)
        {
        }

        protected override DbSet<Buyer> DbSet => DocDb.Buyers;

        public override async Task<Buyer?> RemoveEntityAsync(int id)
        {
            var psa = DocDb.Psas.FirstOrDefault(p => p.Buyer.Id == id);
            if (psa != null)
            {
                return null;
            }

            return await base.RemoveEntityAsync(id);
        }
    }
}
