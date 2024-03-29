﻿namespace Asumet.Doc.Repo
{
    using Asumet.Entities;
    using Microsoft.EntityFrameworkCore;

    internal class SupplierRepository : RepositoryBase<Supplier, int>, ISupplierRepository
    {
        public SupplierRepository(DocDbContext docDb)
            : base(docDb)
        {
        }

        protected override DbSet<Supplier> DbSet => DocDb.Suppliers;

        public override async Task<Supplier?> RemoveEntityAsync(int id)
        {
            var psa = DocDb.Psas.FirstOrDefault(p => p.Supplier.Id == id);
            if (psa != null)
            {
                return null;
            }

            return await base.RemoveEntityAsync(id);
        }
    }
}
