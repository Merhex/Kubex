using System;
using System.IO;
using System.Net;
using System.Text;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Kubex.API.Middleware;
using Kubex.BLL.Services;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL;
using Kubex.DAL.Repositories;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO.Configurations;
using Kubex.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Kubex.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        

        //Configure services for development environment here
        public void ConfigureDevelopmentServices(IServiceCollection services) 
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });

            ConfigureServices(services);
        }

        //Configure services for production environment here
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        //General configuration, gets added both in production and development environments.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling
                    = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            
            services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddUserManager<UserManager<User>>()
                .AddDefaultTokenProviders();
            
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });

            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            
            services.AddAuthorization();

            services.AddCors();

            services.AddScoped<IStreetRepository, StreetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IZIPCodeRepository, ZIPCodeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IMediaTypeRepository, MediaTypeRepository>();
            services.AddScoped<IDailyActivityReportRepository, DailyActivityReportRepository>();
            services.AddScoped<IPriorityRepository, PriorityRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEntryTypeRepository, EntryTypeRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IDailyActivityReportService, DailyActivityReportService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, CloudinaryService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction()) 
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();
            }
            else 
            {
                app.UseExceptionHandler(builder => builder.Run(async context => 
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    context.Response.ContentType = "application/json";

                    var status = exception switch
                    {
                        ApplicationException _ => context.Response.StatusCode = (int)HttpStatusCode.BadRequest,
                        ArgumentNullException _ => context.Response.StatusCode = (int)HttpStatusCode.NotFound,
                        _ => context.Response.StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                    
                    if (status == (int)HttpStatusCode.InternalServerError)
                        await context.Response.WriteAsync(
                            JsonConvert.SerializeObject(new { error = "An unexpected error happend. Please check your data formatting and contact the administrator if this error persists." })
                        );
                    else
                        await context.Response.WriteAsync(result);
                }));
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
