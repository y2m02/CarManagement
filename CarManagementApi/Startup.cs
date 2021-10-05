using System.Linq;
using System.Net;
using CarManagementApi.Helpers;
using CarManagementApi.Models.Entities;
using CarManagementApi.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "CarManagementApi", Version = "v1" }));
            services.AddAutoMapper(option => option.AddProfile<ProfileMapper>());

            services.AddDbContext<CarManagementContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("CarManagementConnection"))
            );

            services
                .AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<CarManagementContext>()
                .AddDefaultTokenProviders();

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
