using Data;
using Data.Entity;
using Data.Repository;
using Domain.Manager;
using Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
            //Repository
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            //Manager
            services.AddScoped<IAdminManager, AdminManager>();
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<IRentalManager, RentalManager>();
            services.AddScoped<ISalesManager, SalesManager>();
            //Mapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookModel, BookEntity>().ReverseMap();
                cfg.CreateMap<AdminModel, AdminEntity>().ReverseMap();
                cfg.CreateMap<CustomerModel, CustomerEntity>().ReverseMap();
                cfg.CreateMap<SalesModel, SalesEntity>().ReverseMap();
                cfg.CreateMap<RentalModel, RentalEntity>().ReverseMap();

            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            //connection
            string connection = Configuration.GetConnectionString("SQLConnection");

            
            services.AddDbContext<LibraryContext>(options =>
                                                     options.UseSqlServer(connection, b => b.MigrationsAssembly("Data")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

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
