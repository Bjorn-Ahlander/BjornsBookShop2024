using BjornsBookShop2024.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BjornsBookShop2024
{ 
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationD
            ///    bContext>();
            //builder.Services.AddControllersWithViews();
            builder.Services.AddDefaultIdentity<BjornBookShopUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            //        .AddEntityFrameworkStores<IdentityDbContext>()
            //        .AddDefaultTokenProviders();

            builder.Services.AddRazorPages();
        builder.Services.AddLogging();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
            options.Password.RequiredUniqueChars = 0;
        });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

        builder.Services.AddControllersWithViews();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();
        using (var scope = app.Services.CreateScope())
        {
            var roleManeger = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "User", "Owner" };
            foreach (var role in roles)
            {
                if (!await roleManeger.RoleExistsAsync(role))
                    await roleManeger.CreateAsync(new IdentityRole(role));
            }
        }
            using (var scope = app.Services.CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<BjornBookShopUser>>();

                string email = "bo.ek@test.se";
                string password = "Schack_2000";
                if (await _userManager.FindByEmailAsync(email) == null)
                {
                    var user = new BjornBookShopUser();
                    user.UserName = email;
                    user.Email = email;
                    await _userManager.CreateAsync(user, password);
                    await _userManager.AddToRoleAsync(user, "Admin");

                }

            }
            app.Run();
    }
}
}
