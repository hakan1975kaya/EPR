using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Extensions;
using Core.IoC.Abstract;
using Core.IoC.Concrete;
using Core.Utilities.Security.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Entities.Utilities.MappingProfiles;
using Microsoft.OpenApi.Models;
using Business.DependencyResolvers.Autofac;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        policy =>
        {
            policy.WithOrigins("*").
            AllowAnyHeader().
            AllowAnyMethod();
        });
});

var configuration = builder.Configuration;

var tokenOptions = configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
        };
    });

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = System.TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddMvc();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EPRApi", Version = "v1.0" });
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = JwtBearerDefaults.AuthenticationScheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
});

builder.Services.AddAutoMapper(typeof(ApiLogMappingProfile));
builder.Services.AddAutoMapper(typeof(AuthMappingProfile));
builder.Services.AddAutoMapper(typeof(CorporateMailAddressMappingProfile));
builder.Services.AddAutoMapper(typeof(CorporateMappingProfile));
builder.Services.AddAutoMapper(typeof(HcpUploadMappingProfile));
builder.Services.AddAutoMapper(typeof(MailAddressMappingProfile));
builder.Services.AddAutoMapper(typeof(MenuMappingProfile));
builder.Services.AddAutoMapper(typeof(MenuOperationClaimMappingProfile));
builder.Services.AddAutoMapper(typeof(OperationClaimMappingProfile));
builder.Services.AddAutoMapper(typeof(PaymentRequestMappingProfile));
builder.Services.AddAutoMapper(typeof(PaymentRequestDetailMappingProfile));
builder.Services.AddAutoMapper(typeof(PaymentRequestSummaryMappingProfile));
builder.Services.AddAutoMapper(typeof(RoleMappingProfile));
builder.Services.AddAutoMapper(typeof(RoleOperationClaimMappingProfile));
builder.Services.AddAutoMapper(typeof(SftpDownloadMappingProfile));
builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(UserRoleMappingProfile));
builder.Services.AddAutoMapper(typeof(WebLogMappingProfile));

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
