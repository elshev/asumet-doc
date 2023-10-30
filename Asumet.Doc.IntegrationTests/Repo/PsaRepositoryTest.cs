using Asumet.Doc.Api;
using Asumet.Doc.Repo;
using Asumet.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Asumet.Doc.IntegrationTests.Repo
{
    public class PsaRepositoryTest : IntegrationApiTestBase
    {
        public PsaRepositoryTest(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async void TestInsertPsa()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var psaRepository = scope.ServiceProvider.GetRequiredService<IPsaRepository>();
            Assert.NotNull(psaRepository);
            var psaToInsert = GetNewPsa();
            LoadPsa(psaToInsert, scope);
            
            // Act
            var psa = await psaRepository.InsertEntityAsync(psaToInsert);

            // Assert
            psa.Should().NotBeNull();
            Assert.NotNull(psa);
            PsasToDelete.Add(psa.Id);

        }

        private async void LoadPsa(Psa psa, IServiceScope scope)
        {
            var buyerRepository = scope.ServiceProvider.GetRequiredService<IBuyerRepository>();
            var supplierRepository = scope.ServiceProvider.GetRequiredService<ISupplierRepository>();

            if (psa.Buyer == null || psa.Buyer.Id == 0)
            {
                var buyers = await buyerRepository.GetAllAsync(); // ToDo: avoid GetAll()
                psa.Buyer = buyers.FirstOrDefault();

            }
            else if (string.IsNullOrEmpty(psa.Buyer.FullName))
            {
                psa.Buyer = await buyerRepository.GetByIdAsync(psa.Buyer.Id);
            }

            if (psa.Supplier == null || psa.Supplier.Id == 0)
            {
                var suppliers = await supplierRepository.GetAllAsync(); // ToDo: avoid GetAll()
                psa.Supplier = suppliers.FirstOrDefault();

            }
            else if (string.IsNullOrEmpty(psa.Supplier.FullName))
            {
                psa.Supplier = await supplierRepository.GetByIdAsync(psa.Supplier.Id);
            }
        }
    }
}
