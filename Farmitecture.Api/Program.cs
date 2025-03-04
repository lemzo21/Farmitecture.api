using System.Reflection;
using Farmitecture.Api.Data;
using Farmitecture.Api.Mapper;
using Farmitecture.Api.Repositories.Interfaces;
using Farmitecture.Api.Repositories.Providers;
using Farmitecture.Api.Services.Interfaces;
using Farmitecture.Api.Services.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);{
    var services = builder.Services;
    var config = builder.Configuration;
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(config.GetConnectionString("DbConnection")));
    services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<ICheckoutService, CheckoutService>();
    builder.Services.AddScoped<IBlogPostService, BlogPostService>();
    builder.Services.AddScoped<ICartService, CartService>();
    services.AddAutoMapper(typeof(ApplicationMapper));
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.DescribeAllParametersInCamelCase();

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization Header using Bearer Security Scheme. \r\r\r\r Enter Bearer [space] and then the security token to authenticate",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },

            []
        }
    });
});


builder.Services.AddAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();