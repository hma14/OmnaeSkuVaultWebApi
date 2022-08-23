﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OmnaeSkuVaultWebApi.Databases;
using OmnaeSkuVaultWebApi.Services;

namespace OmnaeSkuVaultWebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public ICurrentUserService currentUserService { get; set; }
        public IMediator mediator { get; set; }

        public Startup(IConfiguration config, ICurrentUserService currentUserService, IMediator mediator)
        {
            _config = config;
            this.currentUserService = currentUserService;   
            this.mediator = mediator;
        }

        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddMvc();

            services.AddSingleton<IConfiguration>(_config);

            string conn = _config.GetConnectionString("OmnaeFinanceServiceDb");
            services.AddDbContext<FinanceServiceDbContext>(options => options.UseSqlServer(conn));
        }

    }
}