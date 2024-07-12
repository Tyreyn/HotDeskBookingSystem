using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SoftwareMind_Intern_Challenge.Security;
using SoftwareMind_Intern_Challenge.Services;
using SoftwareMind_Intern_ChallengeBL.Operations;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Xml.XPath;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SoftwareMind_Intern_ChallengeDTO.Data.HotDeskBookingSystemContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("default"));
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddTransient<EmployeeOperations>();
builder.Services.AddTransient<DeskOperations>();
builder.Services.AddTransient<LocationOperations>();
builder.Services.AddTransient<ReservationOperations>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<LocationService>();
builder.Services.AddTransient<DeskService>();
builder.Services.AddTransient<ReservationService>();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

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
