using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FiltersMiddlewareExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<LoggerMiddleware>();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/countries", async context =>
                {
                    await GetCountriesAsync(context);
                });
            });

            
        }

        public async Task GetCountriesAsync(HttpContext context)
        {
            var countries = new List<Country>()
            {
                new() {Name = "Japan", Code = "JP"},
                new() {Name = "China", Code = "CH"}
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(countries));
        }
    }

    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var routeValuesStr = JsonSerializer.Serialize(context.Request.RouteValues);
            await context.Response.WriteAsync(
                $"\n Request created with route values: {routeValuesStr} at {DateTime.Now.ToString(CultureInfo.InvariantCulture)} \n");

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
