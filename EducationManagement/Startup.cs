using Business.Abstract;
using Business.Concrete;
using Business.Interface;
using DTO.DTOs.UserDTOs;
using Entity;
using Entity.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Text;
using Utilities;
using Utilities.Helpers;

namespace EducationManagement
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EduContext>(opt => opt.UseInMemoryDatabase("EduDatabase"));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<EduContext>().AddRoleManager<RoleManager<Role>>().AddDefaultTokenProviders();

            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();

            services.AddTransient<ILogonService, LogonService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILessonService, LessonService>();
            services.AddTransient<IEnrollmentService, EnrollmentService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IGenericService<>), typeof(GenericRepository<>));


            services.AddControllers();
            services.AddMvc();

            services.AddSingleton(Configuration);


            services.AddAuthentication();



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Eğitim İşlemleri API",
                    Description = "Bu API, eğitim kurumlarında dönemlik ders seçimi kullanılır.\n\n  **API ile ilgili detaylı bilgiler için** [buraya](" + Configuration["BaseUrl"] + ") tıklayabilirsiniz.",
                    Contact = new OpenApiContact
                    {
                        Name = "Sinan Babalı",
                        Email = "sinannbabali@gmail.com"
                    },
                });

                c.OperationFilter<SwaggerOperationDescriptionFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<EduContext>();
                dbContext.Database.EnsureCreated();
            }

            DatabaseInitializer.Seed(serviceProvider).Wait();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eğitim İşlemleri API v1");
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

        }
    }
}
