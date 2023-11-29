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
        IDisposable,
        IClassFixture<ApiTestWebApplicationFactory<Program>>
    {
        protected ApiTestWebApplicationFactory<Program> Factory { get; }
        
        protected IServiceScope Scope { get; }

        /// <summary> Add here Psa Ids to remove on cleanup </summary>
        protected IList<int> PsasToDelete { get; } = new List<int>();

        public ApiIntegrationTestBase(ApiTestWebApplicationFactory<Program> factory)
            : base()
        {
            Factory = factory;
            Scope = Factory.Services.CreateScope();
        }

        public async void Dispose()
        {
            var psaRepository = GetService<IPsaRepository>();
            foreach (var id in PsasToDelete)
            {
                await psaRepository.RemoveEntityAsync(id);
            }

            Scope.Dispose();
        }

        protected TService GetService<TService>()
            where TService : notnull
        {
            var result = Scope.ServiceProvider.GetRequiredService<TService>();
            return result;
        }
        
        protected static Psa GetPsa(int id = 1)
        {
            return PsaSeedData.GetPsa(id);
        }

        protected static string GetScanFilePath(string fileName)
        {
            var result = Path.Combine("./TestInput/Scan", fileName);
            return result;            
        }

        protected static Psa GetNewPsa()
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
                Buyer = new Buyer { Id = 1 },
                Supplier = new Supplier { Id = 2 },
                PsaScraps = new List<PsaScrap>
                {
                    new() {
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
