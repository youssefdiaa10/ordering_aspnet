using Microsoft.AspNetCore.Mvc;
using Ordering.APIs.Errors;
using Ordering.APIs.Helpers;
using Ordering.Core.Repositories.Interfaces;
using Ordering.Repository.Repositories;

namespace Ordering.APIs.Extention
{
    public static class ApplicationServiceExtention
    {

        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(k => k.Value.Errors.Count() > 0)
                                                         .SelectMany(k => k.Value.Errors)
                                                         .Select(e => e.ErrorMessage)
                                                         .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });


            return services;
        }
    }
}
