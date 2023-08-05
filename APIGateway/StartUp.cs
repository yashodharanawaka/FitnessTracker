using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace APIGateway
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Confguration = configuration;
        }

        private IConfiguration Confguration { get; set; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddOcelot();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseOcelot().Wait();
        }
    }
}
