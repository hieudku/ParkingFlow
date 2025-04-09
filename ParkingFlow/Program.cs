using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParkingFlow.Data;
using ParkingFlow.Models;

namespace ParkingFlow
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Authentication and Authorization setup
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Seed admin role and admin user
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {

                    // Seed Admin role and optionally an Admin user
                    await CreateRoles(services, logger);

                    // Call the method to seed the normal user
                    await CreateNormalUser(services, logger);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the admin role and user.");
                }
            }

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();


            /// <summary>Create and seed admin account and admin role</summary>
            async Task CreateRoles(IServiceProvider serviceProvider, ILogger logger)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    logger.LogInformation("Admin role created");
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var adminEmail = "admin@parkingflow.com";
                var adminPassword = "Admin123!";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    logger.LogInformation("Creating admin user..");

                    var admin = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var createAdminResult = await userManager.CreateAsync(admin, adminPassword);

                    if (createAdminResult.Succeeded)
                    {
                        logger.LogInformation("Admin user created successfully");
                        await userManager.AddToRoleAsync(admin, "Admin");
                    }
                    else
                    {
                        logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", createAdminResult.Errors.Select(e => e.Description)));
                    }
                }
                else
                {
                    logger.LogError("Admin user already exists");
                }
            }

            /// <summary>Seed the normal user account</summary>
            async Task CreateNormalUser(IServiceProvider serviceProvider, ILogger logger)
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Define a regular user's credentials
                var normalUserEmail = "user@parkingflow.com";
                var normalUserPassword = "User123!";

                // Check if the user already exists
                var normalUser = await userManager.FindByEmailAsync(normalUserEmail);

                if (normalUser == null)
                {
                    logger.LogInformation("Creating normal user...");

                    var user = new IdentityUser
                    {
                        UserName = normalUserEmail,
                        Email = normalUserEmail,
                        EmailConfirmed = true
                    };

                    var createUserResult = await userManager.CreateAsync(user, normalUserPassword);

                    if (createUserResult.Succeeded)
                    {
                        logger.LogInformation("Normal user created successfully.");
                    }
                    else
                    {
                        logger.LogError("Failed to create normal user: {Errors}", string.Join(", ", createUserResult.Errors.Select(e => e.Description)));
                    }
                }
                else
                {
                    logger.LogInformation("Normal user already exists.");
                }
            }
        }
    }
}
