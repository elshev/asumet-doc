using Asumet.Common;
using Asumet.Doc.Api;
using Asumet.Doc.Repo;
using Asumet.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Asumet.Doc.IntegrationTests.Repo
{
    public class PsaRepositoryTest : IntegrationTestBase
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

        private Psa GetNewPsa()
        {
            var d = DateTime.Now.SetKindUtc();
            var psa = new Psa
            {
                ActDate = d,
                ActNumber = $"Акт-{d:yyyyMMdd-HHmmss}",
                OwnershipReason = "Личная собственность",
                Transport = "Грузовик",
                ShortScrapDescription = "Какой-то металлолом",
                TotalNetto = 5.125m,
                TotalNettoInWords = "Пять тонн 125 кг",
                Total = 215340.12m,
                TotalInWords = "Двести пятнадцать тысяч триста сорок рублей 12 коп.",
                TotalNds = 0,
                TotalNdsInWords = "Ноль рублей 00 коп.",
                TotalWoNds = 215340.12m,
                Buyer = new Buyer { Id = 3 },
                Supplier = new Supplier { Id = 3 },
                PsaScraps = new List<PsaScrap>
                {
                    new PsaScrap
                    {
                        Name = "Cкрап чугунный незагрязненный",
                        Okpo = "46110003295",
                        GrossWeight = 2.20m,
                        TareWeight = 1.1m,
                        NonmetallicMixtures = 0.1m,
                        MixturePercentage = 8.5m,
                        NetWeight = 1.0m,
                        Price = 100340.12m,
                        SumWoNds = 100340.12m,
                        Sum = 100340.12m
                    }
                }
            };

            return psa;
        }
    }
}
