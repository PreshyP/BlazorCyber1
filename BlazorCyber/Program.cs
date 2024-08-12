using BlazorCyber.Components;
using BlazorCyber.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using BlazorCyber.Platforms.Windows;
using BlazorCyber.Services;
using BlazorCyber;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register ISpeechToText and its implementation
builder.Services.AddSingleton<ISpeechToText, SpeechToTextImplementation>();

// Register other services
builder.Services.AddSingleton<SpeechToTextService>();
builder.Services.AddSingleton<IAppService, AppService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:3000/api/") });

// Add SignalR services
builder.Services.AddSignalR();
builder.Services.AddSingleton<NotifyHub>();
builder.Services.AddSingleton<PanicHub>();
builder.Services.AddSingleton<MySignalRService>();

// Build the application
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();


    app.UseHttpsRedirection();
}
//app.UseCors("CorsPolicy");

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.MapHub<NotifyHub>("/notifyHub");

app.Run();
