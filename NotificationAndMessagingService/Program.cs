using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotificationAndMessagingService;
using NotificationAndMessagingService.Context;
using NotificationAndMessagingService.MiddlewareExtensions;
using NotificationAndMessagingService.Services;
using NotificationAndMessagingService.SubscribeTableDependency;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FitnessContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FitnessConnStr")),ServiceLifetime.Singleton);
builder.Services.AddDbContext<NotificationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NotificationConnStr")), ServiceLifetime.Singleton);
builder.Services.AddSingleton<IUserService,UserService>();

builder.Services.AddSignalR();

builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeNotificationDependency>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Aud"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        option.Events = new JwtBearerEvents
        {
            OnMessageReceived = context => {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken)
                    && path.StartsWithSegments("/notification-hub"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var notificationconnectionString = app.Configuration.GetConnectionString("NotificationConnStr");
app.MapHub<NotificationHub>("notification-hub");

app.UseAuthorization();

app.MapControllers();

app.UseSqlTableDependency<SubscribeNotificationDependency>(notificationconnectionString);


app.Run();
