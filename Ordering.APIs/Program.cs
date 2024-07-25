using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ordering.APIs.Errors;
using Ordering.APIs.Extention;
using Ordering.APIs.Helpers;
using Ordering.APIs.Middelwares;
using Ordering.Core.Models;
using Ordering.Core.Models.Identity;
using Ordering.Core.Models.Services.Interface;
using Ordering.Core.Repositories.Interfaces;
using Ordering.Repository.Data;
using Ordering.Repository.Identity;
using Ordering.Repository.Repositories;
using Ordering.Service;
using StackExchange.Redis;

namespace Ordering.APIs
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Service

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddDbContext<AppIdentityDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });


            //builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            builder.Services.AddApplicationService();

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequiredUniqueChars = 2;
                //options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddScoped<ITokenService, TokenService>();

            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));

            //builder.Services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = (actionContext) =>
            //    {
            //        var errors = actionContext.ModelState.Where(k => k.Value.Errors.Count() > 0)
            //                                             .SelectMany(k => k.Value.Errors)
            //                                             .Select(e => e.ErrorMessage)
            //                                             .ToArray();

            //        var validationErrorResponse = new ApiValidationErrorResponse()
            //        {
            //            Errors = errors
            //        };

            //        return new BadRequestObjectResult(validationErrorResponse);
            //    };
            //});

            builder.Services.AddApplicationService();

            
            #endregion


            //StoreDbContext context = new StoreDbContext();
            //context.Database.MigrateAsync();


            #region Ask CLR to create an Obj. from DbContext in an Exclicit way
            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var service = scope.ServiceProvider;

            var _context = service.GetRequiredService<StoreDbContext>(); // ask CLR to create Obj. from StoreDbContext Explicitly

            var _IdentityDbContext = service.GetRequiredService<AppIdentityDbContext>();


            var _logger = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await _context.Database.MigrateAsync(); // Update Database (Business)

                await StoreDbContextSeed.SeedAsync(_context); // Data Seeding

                await _IdentityDbContext.Database.MigrateAsync(); // Update Database (Identity)


                var _userManager = service.GetRequiredService<UserManager<AppUser>>(); // ask CLR to create Obj. from UserManager<AppUser> Explicitly

                await AppIdentityDbContextSeed.SeedUserAsync(_userManager);
            }
            catch (Exception ex)
            {
               var log = _logger.CreateLogger<Program>();

                log.LogError(ex, "Applying Migration Problem");
                //Console.WriteLine(ex.Message);
            }


            #endregion


            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddelware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
