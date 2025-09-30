using System.Diagnostics;
using System.Text;
using MachineReporting.Api.Jwt;
using MachineReporting.Api.Models.DataBaseContext;
using MachineReporting.Api.Services.Facades;
using MachineReporting.Api.Services.TransactionService.Command;
using MachineReporting.Api.Services.UsersServices.Commands;
using MachineReporting.Api.Services.UsersServices.Facade;
using MachineReporting.Api.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlite("Data Source=machineReporting.db"));
// Add services to the container.

builder.Services.AddScoped<DataBaseContext>();
builder.Services.AddScoped<IUserFacade, UserFacade>();
builder.Services.AddScoped<CreateJwtTokenAsynco>();
builder.Services.AddScoped<ITransactionManagmentService, TransactionManagmentService>();
builder.Services.AddSingleton<ActivitySource>(new ActivitySource("MachineReporting"));
builder.Services.AddTransient<LoggingHandler>();
// services.AddHttpClient("MyClient")
//         .AddHttpMessageHandler<LoggingHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var bearerTokens = builder.Configuration.GetSection("BearerTokens").Get<BearerTokens>();
builder.Services.AddSingleton(bearerTokens);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.AutomaticRefreshInterval = TimeSpan.FromHours(5);
    options.TokenValidationParameters = new()
    {

        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidIssuer = bearerTokens.Issuer,
        ValidAudience = bearerTokens.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(bearerTokens.Key))
    };
});

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddSource("MachineReporting")
            .AddConsoleExporter();
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<RequestTimingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
