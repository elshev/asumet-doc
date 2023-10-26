using Asumet.Doc.Repo;

namespace Asumet.Doc.Api
{
    internal static class DocDbInitializerExtension
    {
        public static IApplicationBuilder SeedDocDb(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DocDbContext>();
                DocDbInitializer.Initialize(context);
            }
            catch (Exception)
            {

            }

            return app;
        }
    }
}