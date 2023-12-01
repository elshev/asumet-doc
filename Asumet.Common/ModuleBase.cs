namespace Asumet.Doc
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Base class for initializing modules.
    /// </summary>
    public abstract class ModuleBase
    {
        /// <summary>
        /// Is Module already initialized for the services
        /// </summary>
        /// <remarks>ToDo: Consider to use WeakReference here</remarks>
        private readonly List<IServiceCollection> IsInitializedList = new();
        
        private readonly object initLock = new();

        /// <summary>
        /// Initializes the module
        /// </summary>
        /// <param name="services">Services to configure</param>
        /// <param name="configuration">Application configuration</param>
        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            if (IsInitializedList.IndexOf(services) > -1)
            {
                return;
            }

            lock (initLock)
            {
                if (IsInitializedList.IndexOf(services) > -1)
                {
                    return;
                }

                InternalInitialize(services, configuration);

                IsInitializedList.Add(services);
            }
        }

        /// <summary>
        /// The essential part of the module initialization
        /// </summary>
        /// <param name="services">Services to configure</param>
        /// <param name="configuration">Application configuration</param>
        protected abstract void InternalInitialize(IServiceCollection services, IConfiguration configuration);
    }
}
