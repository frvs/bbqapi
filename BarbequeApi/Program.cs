using BarbequeApi.Models;
using BarbequeApi.Repositories;
using BarbequeApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
  options.UseInMemoryDatabase("fakeConnectionString");
});

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IBarbequeRepository, BarbequeRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IBarbequeService, BarbequeService>();

var app = builder.Build();
app.MapGet("/", () => { Results.LocalRedirect("/swagger/index.html", true, true); }).ExcludeFromDescription();

Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void Seed()
{
  using (var scope = app.Services.CreateScope())
  {
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.EnsureCreated();

    dataContext.Barbeques.Add(new Barbeque
    {
      Title = "Comemoracao de job novo",
      Date = DateTime.Now,
      Persons = new List<Person>
      {
        new Person
        {
          Name = "Lucas Frvs",
          BeverageMoneyShare = 50,
          FoodMoneyShare = 20
        }
      }});

    dataContext.SaveChanges();
  }
}

public partial class Program { }
