using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MyULibraryBackend.Entities;
using MyULibraryBackend.Repositories;
using MyULibraryBackend.Repositories.Implementation;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace MyULibraryBackend
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
            services.AddDbContext<MyULibraryDbContext>(o => o.UseSqlServer(Configuration["ConnectionStrings:MyULibrary"]));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IBookLogRepository, BookLogRepository>();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200", "https://my-u-library-frontend.azurewebsites.net").AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(env.ContentRootPath, "StaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
