using MeDirect.LightsOut.Data.Repository;
using MeDirect.LightsOut.Data.Settings;
using MeDirect.LightsOut.Service.Configuration;
using MeDirect.LightsOut.Service.GamePlay;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeDirect.LightsOut.Api.Configuration
{
    /// <summary>
    /// Dependancy Injection COnfiguration
    /// </summary>
    public static class DependancySetup
    {
        /// <summary>
        /// Configure DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureDependancies(
            this IServiceCollection services, IConfiguration configuration)
        {
            // configuration
            services.AddSingleton(configuration);
            services.AddScoped<IApiConfiguration, ApiConfiguration>();  // !Important Must be Scoped (Per request) 
            
            // repositories
            services.AddTransient<ILoggingRepository, LoggingRepository>();
            services.AddTransient<IGameConfigurationRepository, GameConfigurationRepository>();
            services.AddTransient<IGameScoresRepository, GameScoresRepository>();
            
            // services
            services.AddTransient<IBoardService, BoardService>();
            services.AddTransient<IMoveService, MoveService>();
            services.AddScoped<IGameConfigurationService, GameConfigurationService>();
            services.AddTransient<IScoreService, ScoreService>();
         
            return services;
        }
    }
}