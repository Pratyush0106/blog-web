//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using BlogApi.Data;
//using System.Text;
//using blogapi.Controllers;
//using blogapi.Service;
//using Org.BouncyCastle.Pqc.Crypto.Lms;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogApi", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Please enter a valid token",
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        BearerFormat = "JWT",
//        Scheme = "Bearer"
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});

//// Read JWT settings from configuration
//var jwtSettings = builder.Configuration.GetSection("jwtSettings");
//var key = jwtSettings["key"];
//var issuer = jwtSettings["issuer"];
//var audience = jwtSettings["audience"];

//// Configure Identity
////builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
////{
////    options.SignIn.RequireConfirmedEmail = true;
////    options.Lockout.AllowedForNewUsers = true;
////    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(4);
////    options.Lockout.MaxFailedAccessAttempts = 3;
////})
////    .AddEntityFrameworkStores<AppDbContext>()
////    .AddDefaultTokenProviders();

//// Configure JWT authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = issuer,
//            ValidAudience = audience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
//        };
//    });

//// Configure role-based authorization
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
//    options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));
//});

//// Register AppDbContext
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));

//// Configure CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        builder =>
//        {
//            builder.AllowAnyOrigin()
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});

//// Email notification
//builder.Services.AddHttpClient();
//builder.Services.AddScoped<IContactFormService, ContactFormService>();
//builder.Services.AddSingleton<EmailService>();

//builder.Logging.ClearProviders(); // Optional: Clears default logging providers
//builder.Logging.AddConsole(); // Adds console logging provider
//builder.Logging.AddDebug(); // Adds Debug logging provider

//// Set specific log levels for different libraries
//builder.Logging.SetMinimumLevel(LogLevel.Information); // Default minimum level
//builder.Logging.AddFilter("Microsoft", LogLevel.Warning); // For Microsoft.* namespaces
//builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning); // For ASP.NET Core
//builder.Logging.AddFilter("System", LogLevel.Warning); // For System.* namespaces
//builder.Logging.AddFilter("OnlineCommunityPlatform", LogLevel.Trace);

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogApi v1");
//    });
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseCors("AllowAllOrigins"); // Apply the CORS policy

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BlogApi.Data;
using System.Text;
using blogapi.Controllers;
using blogapi.Service;
using Org.BouncyCastle.Pqc.Crypto.Lms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Read JWT settings from configuration
var jwtSettings = builder.Configuration.GetSection("jwtSettings");
var key = jwtSettings["key"];
var issuer = jwtSettings["issuer"];
var audience = jwtSettings["audience"];

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

// Configure role-based authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
    options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));

});

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Email notification
builder.Services.AddHttpClient();
builder.Services.AddScoped<IContactFormService, ContactFormService>();
builder.Services.AddSingleton<EmailService>();

builder.Logging.ClearProviders(); // Optional: Clears default logging providers
builder.Logging.AddConsole(); // Adds console logging provider
builder.Logging.AddDebug(); // Adds Debug logging provider

// Set specific log levels for different libraries
builder.Logging.SetMinimumLevel(LogLevel.Information); // Default minimum level
builder.Logging.AddFilter("Microsoft", LogLevel.Warning); // For Microsoft.* namespaces
builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning); // For ASP.NET Core
builder.Logging.AddFilter("System", LogLevel.Warning); // For System.* namespaces
builder.Logging.AddFilter("OnlineCommunityPlatform", LogLevel.Trace);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogApi v1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseCors("AllowAllOrigins"); // Apply the CORS policy

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

