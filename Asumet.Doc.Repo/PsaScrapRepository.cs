namespace Asumet.Doc.Repo
{
    using Asumet.Entities;
    using Microsoft.EntityFrameworkCore;

    internal class PsaScrapRepository : RepositoryBase<PsaScrap, int>, IPsaScrapRepository
    {
        public PsaScrapRepository(DocDbContext docDb)
            : base(docDb)
        {
        }

        protected override DbSet<PsaScrap> DbSet => DocDb.PsaScraps;
    }
}
