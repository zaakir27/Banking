using Banking.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebManagement.Api.Extensions;

namespace Banking.Api
{
    public class Startup
    {
        private IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment   = environment;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerSupport();
            services.AddCors(options => options.AddPolicy("KnownCORS", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            
            // Register Repositories
            services.RegisterRepositories();

            // Register Services / Providers
            services.RegisterServices();
            
            //var configSetting = new ConfigurationSettings();
            //var connection    = Configuration.GetSection("DATA");
            //connection.Bind(configSetting);

            //services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());
            
           // DbContext.ConnectionName = configSetting.ConnectionString ?? throw new ArgumentNullException(nameof(configSetting.ConnectionString));
            
            //var masterConnection = new SqliteConnection(DbContext.ConnectionName);
            //masterConnection.Open();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }
            else
            {
                app.UseHttpsRedirection();
            }
            
            app.UseRouting();
            app.UseCors("KnownCORS");
            app.UseSerilogRequestLogging();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}