using UrlShortener.DAL.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.ServiceAbstractions;
using UrlShortener.Application.Services;
using UrlShortener.DAL.Repositories;
using UrlShortener.DAL.RepositoryAbstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UrlShortenerDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IUrlShortenerRepository<>),typeof(UrlShortenerRepository<>));
builder.Services.AddScoped<IShortenUrlRepository, ShortenUrlRepository>();

builder.Services.AddScoped<IShortenUrlService, ShortenUrlService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();