namespace SecondHandClothes
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SecondHandClothes.Data;
    using SecondHandClothes.Data.Models;
    using SecondHandClothes.Infrastructure;
    using SecondHandClothes.Services;
    using SecondHandClothes.Services.Cart;
    using SecondHandClothes.Services.Cart.Models;
    using SecondHandClothes.Services.Home;
    using SecondHandClothes.Services.Orders;
    using SecondHandClothes.Services.Products;
    using SecondHandClothes.Services.Sellers;
    using SecondHandClothes.Services.Statistics;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<SecondHandDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SecondHandDbContext>();
            services.AddMemoryCache();

            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                });
                
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IOrderService, OrderService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });
        }
    }
}
