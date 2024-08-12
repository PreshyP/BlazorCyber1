using BlazorCyber;
using BlazorCyber.Hubs;
using BlazorCyber.Platforms.Windows;
using BlazorCyber.Services;
using Example;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();

        // Add services for controllers with views
        services.AddControllersWithViews();

        // Add services for Razor Pages
        services.AddRazorPages();

        // Add CORS policy
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials()
                                  .WithOrigins("http://localhost:3000")); // Adjust origin as per your Blazor app URL
        });

        // Add other necessary services
        services.AddRazorComponents().AddInteractiveServerComponents();
        services.AddSingleton<ISpeechToText, SpeechToTextImplementation>();
        services.AddSingleton<SpeechToTextService>();
        services.AddSingleton<IAppService, AppService>();
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:3000/api/") });
        services.AddSingleton<NotifyHub>();
        services.AddSingleton<PanicHub>();
        services.AddSingleton<MySignalRService>();

        // Register MyService and its dependencies
       // services.AddSingleton<IDependencyA, DependencyAImplementation>(); // Register IDependencyA
        services.AddSingleton<MyService>(); // Register MyService
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseCors("CorsPolicy");


        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("CorsPolicy"); // Apply CORS policy here
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapBlazorHub(); // Map Blazor Hub
            endpoints.MapHub<NotifyHub>("/notifyHub"); // Map SignalR Hub
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}
