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

        public override Buyer? RemoveEntity(int id)
        {
            var psa = DocDb.Psas.FirstOrDefault(p => p.Buyer.Id == id);
            if (psa != null)
            {
                return null;
            }

            return base.RemoveEntity(id);
        }
    }
}
