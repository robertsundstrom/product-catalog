﻿using CatalogTest;
using CatalogTest.Data;

using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//builder.Services.AddSqlite<CatalogContext>("Data Source=mydb.db");

builder.Services.AddSqlServer<CatalogContext>(configuration.GetConnectionString("mssql"));

// Register the Swagger services
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Web API";
    config.Version = "v1";
});

builder.Services.AddScoped<Api>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    app.UseOpenApi();
    app.UseSwaggerUi3(c => c.DocumentTitle = "Web API v1");
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

//await Seed.SeedAsync(app.Services);

app.Run();