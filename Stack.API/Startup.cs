using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Stack.API.AutoMapperConfig;
using Microsoft.OpenApi.Models;
using Stack.API.Extensions;
using System.Text;
using Stack.DAL;
using Stack.Core;
using Stack.Entities.Models.Modules.Auth;
using Stack.Core.Managers.Modules.Auth;
using Stack.DTOs.Models.Initialization.ActivityTypes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stack.API.Hubs;
using Hangfire;

namespace Stack.API
{
    public class Startup
    {

        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<List<ActivityTypeModel>>(Configuration.GetSection("DefaultActivityTypes"));

            services.Configure<List<DTOs.Requests.Modules.System.AuthorizationSection>>(Configuration.GetSection("SystemAuthorizationSections"));


            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Live server connection string
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=64.112.57.179; Database = CRMDB; User Id = sa; Password = P@ssw0rd$$.;"));
            //services.AddHangfire(x => x.UseSqlServerStorage("Server=64.112.57.179; Database = CRMDB; User Id = sa; Password = P@ssw0rd$$.;"));


            //Local server connection string
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=NaderHosny; Database=CRMDB;User ID=sa;Password=P@ssw0rd$$.;"));
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=Amr\\SQLEXPRESS; Database = CRMDB; User Id = SA; Password = P@ssw0rd;"));
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server= B-YASMIN-GHAZY\\SQLEXPRESS; Database = CRMDB; User Id = SA; Password = P@ssw0rd;"));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server= DESKTOP-HH8V8OH; Database = CRMDB;Integrated Security =true;MultipleActiveResultSets=True; "));


            //Hangfire connection string
            //Local connection string
            services.AddHangfire(x => x.UseSqlServerStorage("Server=NaderHosny; Database=CRMDB;User ID=sa;Password=P@ssw0rd$$.;"));
            //services.AddHangfire(x => x.UseSqlServerStorage("Server=Amr\\SQLEXPRESS; Database = CRMDB; User Id = SA; Password = P@ssw0rd;"));

            services.AddHangfireServer();

            //Add Identity framework.
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>();


            //CORS Configuration . 
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                             builder =>
                             {
                                 builder.WithOrigins("http://localhost:4200", "http://localhost:4201", "https://localhost:4200")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials();
                             });
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: AllowSpecificOrigins,
            //                 builder =>
            //                 {
            //                     builder.WithOrigins("https://crm.app-blender.com")
            //                        .AllowAnyMethod()
            //                        .AllowAnyHeader()
            //                        .AllowCredentials();
            //                 });
            //});

            //Configure Auto Mapper .
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddAutoMapper(typeof(ActivitiesMapperProfile));


            /////////////////////////////////////////

            services.AddScoped<UnitOfWork>();

            services.AddBusinessServices();

            //Add and configure JWT Bearer Token Authentication . 
            services.AddAuthentication(options => options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Token:Key").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                //Record Lock Hub
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/recordLockHub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };


                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

                });
            });

            ///Use Swagger .
            ConfigureSwagger(services);

            services.AddControllers();

            services.AddSignalR();

        }

        //Configure Swagger .
        private static void ConfigureSwagger(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Stack",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                           {
                             new OpenApiSecurityScheme
                             {
                               Reference = new OpenApiReference
                               {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                               }
                              },
                              new string[] { }
                            }
                  });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseDeveloperExceptionPage();


            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

            });


            //Use CORS 
            app.UseCors(AllowSpecificOrigins);

            app.UseRouting();

            // using authentication middleware

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<RecordLockHub>("/recordLockHub");
            });

            app.UseHangfireDashboard("/mydashboard");


        }

    }
}
