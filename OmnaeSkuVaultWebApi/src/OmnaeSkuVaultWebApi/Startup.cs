using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OmnaeSkuVaultWebApi.Databases;
using OmnaeSkuVaultWebApi.Extensions.Services;
using OmnaeSkuVaultWebApi.Services;
using SkuVaultApiWrapper;
using SkuVaultApiWrapper.DependencyInjection;
using SkuVaultApiWrapper.Models;

namespace OmnaeSkuVaultWebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public ICurrentUserService currentUserService { get; set; }
        public IMediator mediator { get;  }
        public HttpClient httpClient { get;  }

        public Startup(IConfiguration config, ICurrentUserService currentUserService, IMediator mediator, HttpClient httpClient)
        {
            _config = config;
            this.currentUserService = currentUserService;   
            this.mediator = mediator;
            this.httpClient = httpClient;
        }

        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_config);

            services.AddControllers().AddNewtonsoftJson();
            services.AddApiVersioningExtension();


            



#if false
            string conn = _config.GetConnectionString("OmnaeFinanceServiceDb");
            services.AddDbContext<FinanceServiceDbContext>(options => options.UseSqlServer(conn));
#else

            services.AddDbContext<FinanceServiceDbContext>(options =>
                    options.UseSqlServer(
                        _config.GetConnectionString("OmnaeFinanceServiceDb"),
                        builder => builder.MigrationsAssembly(typeof(FinanceServiceDbContext).Assembly.FullName)));
#endif

            services.AddControllers().AddControllersAsServices();
        }

    }
}
