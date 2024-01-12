using Infrastructure.Interfaces;
using Infrastructure.Repositores;
using Microsoft.AspNetCore.Mvc;
using Services.Services.ProductService.Dto;
using Services.Services.ProductService;
using Demo.HandelResponses;
using Services.Services.CaheService;
using Services.Services.CacheService;
using Services.Services.BasketService.Dto;
using Infrastructure.BasketRepository;
using Services.Services.BasketService;
using Services.Services.TokenService;
using Services.UserService;
using Services.Services.OrderService.Dto;
using Services.Services.PaymentService;
using Services.Services.OrderService;

namespace Demo.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                .Where(model => model.Value.Errors.Count > 0)
                                .SelectMany(model => model.Value.Errors)
                                .Select(error => error.ErrorMessage).ToList();
                    var errorRespone = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorRespone);
                };
            });

            //builder.Services.AddAutoMapper(x => x.AddProfile(new ProductProfile()));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));

            return services;
        }
    }
}
