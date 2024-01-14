using AFC.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AFC.Api;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string connection = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<AfcDbContext>(options => options.UseSqlServer(connection));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //services.AddSwaggerGen(c =>
        //{
        //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Afc Api Management", Version = "v1.0" });

        //    var securityScheme = new OpenApiSecurityScheme
        //    {
        //        Name = "Akbank Final Case",
        //        Description = "Enter JWT Bearer token **_only_**",
        //        In = ParameterLocation.Header,
        //        Type = SecuritySchemeType.Http,
        //        Scheme = "bearer",
        //        BearerFormat = "JWT",
        //        Reference = new OpenApiReference
        //        {
        //            Id = JwtBearerDefaults.AuthenticationScheme,
        //            Type = ReferenceType.SecurityScheme
        //        }
        //    };
        //    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
        //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //        { securityScheme, new string[] { } }
        //    });
        //});
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(x => { x.MapControllers(); });
    }
}
