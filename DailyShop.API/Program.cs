using DailyShop.Business;
using Core.Security;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.DataAccess;
using DailyShop.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application;
using Core.Security.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Core.Security.JWT;
using System.Text.Json.Serialization;
using DailyShop.API.Helpers;
using DailyShop.Business.Middlewares.Auth;
using DailyShop.Business.Services.AuthService;
using DailyShop.DataAccess.Concrete.Dapper.Contexts;
using Microsoft.AspNetCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
//	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
//builder.Services.AddApplicationServices();

builder.Services.AddSecurityServices();
builder.Services.AddApplicationServices();
builder.Services.AddDbContext<DailyShopContext>(opt =>
{
	opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddPersistanceRegistration();
builder.Services.GetConfiguration(builder.Configuration);
builder.Services.AddScoped<DbContext, DailyShopContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddTransient<ImageHelper>();

TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}) // Authentication: "Bearer JWT_TOKEN"
	   .AddJwtBearer(options =>
	   {
		   options.TokenValidationParameters = new TokenValidationParameters
		   {
			   ValidateIssuer = true,
			   ValidIssuer = tokenOptions?.Issuer,
			   ValidateAudience = true,
			   ValidAudience = tokenOptions?.Audience,
			   ValidateLifetime = true,
			   ValidateIssuerSigningKey = true,
			   IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions?.SecurityKey),
			   ClockSkew = TimeSpan.Zero
		   };

		   options.Events = new JwtBearerEvents
		   {
			   OnChallenge = context =>
			   {
				   Console.WriteLine("OnChallange: ");
				   return Task.CompletedTask;
			   },
			   OnAuthenticationFailed = context =>
			   {
				   Console.WriteLine("OnAuthenticationFailed:");
				   return Task.CompletedTask;
			   },
			   OnMessageReceived = context =>
			   {
				   Console.WriteLine("OnMessageReceived:");
				   return Task.CompletedTask;
			   },
			   OnTokenValidated = context =>
			   {
				   Console.WriteLine("OnTokenValidated:");
				   return Task.CompletedTask;
			   },
		   };
	   })
	   .AddCookie();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen(opt =>
{
	opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description =
			"JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
	});
	opt.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
				{ Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
			new string[] { }
		}
	});
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options => {
	options.AddPolicy(MyAllowSpecificOrigins, policyBuilder => {
		policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
	});
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	//app.UseSwaggerUI();
	app.UseSwaggerUI(opt => { opt.DisplayRequestDuration(); opt.SwaggerEndpoint("/swagger/v1/swagger.json", "DailyShop"); });
}
//if (app.Environment.IsProduction())
	//app.ConfigureCustomExceptionMiddleware();

//app.ConfigureCustomAuthExceptionMiddleware();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages")),
    RequestPath = "/wwwroot/ProductImages"
});
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WebSiteIcons")),
    RequestPath = "/wwwroot/WebSiteIcons"
});
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImages")),
    RequestPath = "/wwwroot/UserImages"
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
