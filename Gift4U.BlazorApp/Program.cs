using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Gift4U.BlazorApp.Data;
using Gift4U.Application.Services;
using Gift4U.Domain.Models;
using Gift4U.BlazorApp.Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<CounterStore>();
builder.Services.AddSingleton<GiftStore>();
builder.Services.AddSingleton<TimerStore>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<GiftService>();

builder.Services.AddDbContextFactory<GiftDBContext>(opt =>
    opt.UseSqlite($"Data Source=GiftDB.db"));

var app = builder.Build();

using (var dbSetupScope = app.Services.CreateScope())
{
    dbSetupScope.ServiceProvider.GetService<GiftDBContext>().Database.Migrate();
}
    // new comment
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
