namespace Asumet.Doc.Repo
{
    using Asumet.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class BuyerRepository : RepositoryBase<Buyer, int>, IBuyerRepository
    {
        public override Task<IEnumerable<Buyer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Buyer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Buyer? RemoveEntity(int id)
        {
            throw new NotImplementedException();
        }

        public override Buyer? UpdateEntity(Buyer entity)
        {
            throw new NotImplementedException();
        }
    }
}
