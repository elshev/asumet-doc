namespace Asumet.Doc.IntegrationTests
{
    using Asumet.Common;
    using Asumet.Doc.Api;
    using Asumet.Doc.Repo;
    using Asumet.Entities;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A base class for all tests related to WebApi.
    /// Loads and configures all the infrastructure which is set up in <see cref="Program"/>
    /// </summary>
    public abstract class ApiIntegrationTestBase : 
        IntegrationTestBase,
        IClassFixture<ApiTestWebApplicationFactory<Program>>
    {
        protected ApiTestWebApplicationFactory<Program> Factory { get; }

        /// <summary> Add here Psa Ids to remove on cleanup </summary>
        protected IList<int> PsasToDelete { get; } = new List<int>();

        public ApiIntegrationTestBase(ApiTestWebApplicationFactory<Program> factory)
            : base()
        {
            Factory = factory;
        }

        public override void Dispose()
        {
            using var scope = Factory.Services.CreateScope();
            var psaRepository = scope.ServiceProvider.GetRequiredService<IPsaRepository>();
            foreach (var id in PsasToDelete)
            {
                psaRepository.RemoveEntity(id);
            }

            base.Dispose();
        }

        protected Psa GetNewPsa()
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
