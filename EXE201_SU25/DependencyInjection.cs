using BusinessObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.Context;
using Services;
using Services.Interfaces;
using Services.Service;
using System.Text;
using ViewModels.Momo;

namespace EXE201_SU25
{
    public static class DependencyInjection
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigRoute();
            services.AddSwaggerGen();
            services.AddIdentity();
            services.AddInfrastructure(configuration);
            services.AddAuthentication(configuration);
            services.AddServices(configuration);
        }

        public static void ConfigRoute(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Authentication:Jwt:Issuer"],
                    ValidAudience = configuration["Authentication:Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:Key"])),
                };
            });
        }

        public static void AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.EnableAnnotations();
                option.DescribeAllParametersInCamelCase();
                option.ResolveConflictingActions(conf => conf.First());
                option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id=JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Users, Roles>()
             .AddEntityFrameworkStores<EXE201_SU25DbContext>()
             .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
            });
        }

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogTypeService, BlogTypeService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IRecycleGuideService, RecycleGuideService>();
            services.AddScoped<IRecycleLocationService, RecycleLocationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWasteService, WasteService>();
            services.AddScoped<IWasteTypeService, WasteTypeService>();
            services.AddScoped<ICampaignsService, CampaignsService>();
            services.AddScoped<ITransactionLogService, TransactionLogService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IMomoService, MomoService>();
            services.AddScoped<IRoleService, RoleService>();
            services.Configure<MomoOptionModel>(configuration.GetSection("Momo"));
            services.AddScoped<IPostService, PostService>();
        }
    }
}
