using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikestoreAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace bikestoreAPI
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
            //services.AddDbContext<StoreContext>(opt =>
            //    opt.UseInMemoryDatabase("bikestoreAPI"));

            //services.AddDbContext<StoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StoreContext")));

            var connection = @"Server=WIN10;Database=bikestoreAPI;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(connection));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
