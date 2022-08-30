using Serilog;
using OmnaeSkuVaultWebApi.Extensions.Application;
using OmnaeSkuVaultWebApi.Extensions.Host;
using OmnaeSkuVaultWebApi.Extensions.Services;
using OmnaeSkuVaultWebApi;
using OmnaeSkuVaultWebApi.Controllers.v1;
using SkuVaultApiWrapper.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggingConfiguration(builder.Environment);

builder.ConfigureServices();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    //p.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// For elevated security, it is recommended to remove this middleware and set your server to only listen on https.
// A slightly less secure option would be to redirect http to 400, 505, etc.
app.UseHttpsRedirection();

app.UseCors("OmnaeSkuVaultWebApiCorsPolicy");

app.UseSerilogRequestLogging();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/api/health");
    endpoints.MapControllers();
});

app.UseSwaggerExtension();

app.MapControllers();
app.Run();

try
{
    Log.Information("Starting application");
    await app.RunAsync();
}
catch (Exception e)
{
    Log.Error(e, "The application failed to start correctly");
    throw;
}
finally
{
    Log.Information("Shutting down application");
    Log.CloseAndFlush();
}


// Make the implicit Program class public so the functional test project can access it
public partial class Program {
    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
                
}           