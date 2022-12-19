using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NGNotesAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NGNotesAPI.Services;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NGNotesAPI
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
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(options =>
            //    {
            //        Configuration.Bind("AzureAdB2C", options);

            //        options.TokenValidationParameters.NameClaimType = "name";
            //    },

            //    options => { Configuration.Bind("AzureAdB2C", options); });

            services.AddScoped<IUserService, DefaultUserService>();

            services.AddScoped<INoteService, DefaultNoteService>();

            services.AddScoped<IProjectService, DefaultProjectService>();

            services.AddControllers();

            services.AddDbContext<NGNotesApiDBContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("NGNotesSQL")));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "NG Notes API",
                    Description = "RESTFUL Web API for the NG Notes iOS Mobile Applicaiton"
                });
            });

            var mappingConfig = new MapperConfiguration(mappingConfig =>
            {
                mappingConfig.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "NG Notes API V1");
            });

            // Dev Only
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UsePathBase("/NGNotes");

            // Auth
            //app.UseAuthentication();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Dev Only
            app.UseCors("AllowAll");
        }
    }
}
