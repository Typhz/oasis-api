using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Oasis.API.Middlewares;
using Oasis.API.Seed;
using Oasis.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
	.AddJsonOptions(
		options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
	);

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
	policy =>
	{
		policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
	});
});
builder.Services.AddDbContext<EntityContext>(options =>
	options.UseNpgsql("Host=localhost:5432;Database=oasis_dev;Username=postgres;Password=root"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Insira o token JWT desta maneira: Bearer {seu token}"
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
								}
						},
						Array.Empty<string>()
				}
		});
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			// TODO: Change this to a more secure way in .ENV
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1c00a1c21bab75249256cfbe41192cd7")),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});


var app = builder.Build();
app.UseCors();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<AuthMiddleware>();

DbInitializer.Initialize(app.Services);

app.MapControllers();

app.Run();
