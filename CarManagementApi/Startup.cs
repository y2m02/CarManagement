using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarManagementApi.Helpers;
using CarManagementApi.Models.Entities;
using CarManagementApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CarManagementApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRouting(option => option.LowercaseUrls = true);
            services.AddAutoMapper(option => option.AddProfile<ProfileMapper>());

            services.AddSwaggerGen(
                option =>
                {
                    option.SwaggerDoc("v1", new() { Title = "CarManagementApi", Version = "v1" });

                    option.AddSecurityDefinition(
                        name: "Bearer",
                        securityScheme: new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Access token",
                            Name = "Authorization",
                            Type = SecuritySchemeType.Http,
                            BearerFormat = "JWT",
                            Scheme = "bearer",
                        }
                    );

                    option.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            [new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                            }] = new List<string>(),
                        }
                    );
                }
            );

            services.AddDbContext<CarManagementContext>(
                options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(Configuration.GetConnectionString("CarManagementConnection"));
                }
            );

            services
                .AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<CarManagementContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(
                    option =>
                    {
                        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(
                    option =>
                    {
                        option.SaveToken = true;

                        option.TokenValidationParameters = new()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration["JWT:ValidIssuer"],
                            ValidateAudience = true,
                            ValidAudience = Configuration["JWT:ValidAudience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])
                            ),
                        };
                    }
                );

            var names = new[] { "Service", "Repository" };

            services.Scan(
                scanner =>
                    scanner
                        .FromAssemblies(typeof(BrandRepository).Assembly)
                        .AddClasses(x => x.Where(c => names.Any(name => c.Name.EndsWith(name))))
                        .AsMatchingInterface()
                        .WithScopedLifetime()
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarManagementApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
