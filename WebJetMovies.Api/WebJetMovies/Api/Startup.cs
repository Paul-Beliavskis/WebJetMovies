using System;
using System.IO;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using WebJetMovies.Api.Constants;
using WebJetMovies.Api.Extensions;
using WebJetMovies.Api.Policies;
using WebJetMovies.Application.Intefaces;
using WebJetMovies.Infrastructure.Configuration;
using WebJetMovies.Infrastructure.Services;

namespace WebJetMovies.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger.Information("StartUp");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebJetMovies", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny",
                builder =>
                {
                    builder.WithOrigins("*");
                });
            });


            AssemblyScanner
                .FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddHttpClient<IMovieApiClient, MovieApiClient>((client) =>
            {
                //Token that is retrieved from environment variables (not included in GitHub
                //and not sent to the UI for security. Instead app service has it set as an env variable)
                client.DefaultRequestHeaders.Add(ApiConstants.RequestHeaders.AccessTokenHeaderName, Configuration[ApiConstants.ConfigurationNames.AccessToken]);

                Uri.TryCreate(Configuration[ApiConstants.ConfigurationNames.ApiServerAddress], UriKind.Absolute, out Uri baseAddress);

                client.BaseAddress = baseAddress;
                client.Timeout = TimeSpan.FromSeconds(5);

            }).SetHandlerLifetime(TimeSpan.FromMinutes(5))
                    .AddPolicyHandler(RetryPolicy.GetRetryPolicy());

            services.Configure<MovieApiUris>(Configuration.GetSection(ApiConstants.ConfigurationNames.MovieApiUris));
            services.Configure<ApiRequestDetails>(Configuration.GetSection(ApiConstants.ConfigurationNames.ApiRequestDetails));

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddSingleton(Log.Logger);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAny");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebJetMovies v1"));
            }

            app.UseErrorHandlingMiddleware();

            //This will ensure that if no route is found we need to return the UI page
            app.UseRouteHandlerMiddleware();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Api/wwwroot"))
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
