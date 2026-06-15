using eTickets.Models;
using eTickets.Models.IdentityEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using E_tickets.Core.Domain; 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_tickets.Infrastructure.Data;

namespace E_tickets.Infrastructure.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                context.Database.EnsureCreated();

                // 1. Cinemas
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema("Cinema 1", "/images/cinemas/32.jpeg", "This is the description of the first cinema"),
                        new Cinema("Cinema 2", "/images/cinemas/2.png", "This is the description of the second cinema"),
                        new Cinema("Cinema 3", "/images/cinemas/32.jpeg", "This is the description of the third cinema"),
                        new Cinema("Cinema 4", "/images/cinemas/222.png", "This is the description of the fourth cinema"),
                        new Cinema("Cinema 5", "/images/cinemas/qwww.jpeg", "This is the description of the fifth cinema")
                    });
                    await context.SaveChangesAsync();
                }

                // 2. Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor("Actor 1", "/images/actors/1.jpeg", "This is the Bio of the first actor"),
                        new Actor("Actor 2", "/images/actors/download (1).jpeg", "This is the Bio of the second actor"),
                        new Actor("Actor 3", "/images/actors/download (2).jpeg", "This is the Bio of the third actor"),
                        new Actor("Actor 4", "/images/actors/download (3).jpeg", "This is the Bio of the fourth actor"),
                        new Actor("Actor 5", "/images/actors/download (2).jpeg", "This is the Bio of the fifth actor")
                    });
                    await context.SaveChangesAsync();
                }

                // 3. Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer("Producer 1", "images/producers/images.jpeg", "This is the Bio of the first producer"),
                        new Producer("Producer 2", "images/producers/images.jpeg", "This is the Bio of the second producer"),
                        new Producer("Producer 3", "images/producers/Will-Packer-Producer.jpg", "This is the Bio of the third producer"),
                        new Producer("Producer 4", "images/producers/images.jpeg", "This is the Bio of the fourth producer"),
                        new Producer("Producer 5", "images/producers/Will-Packer-Producer.jpg", "This is the Bio of the fifth producer")
                    });
                    await context.SaveChangesAsync();
                }

                // 4. Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie("Life", "This is the Life movie description", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10), 39.50, "/images/movies/life.jpg", MovieCategory.Documentary, 3, 3),
                        new Movie("The Shawshank Redemption", "This is the Shawshank Redemption description", DateTime.Now, DateTime.Now.AddDays(3), 29.50, "/images/movies/TheShawshankRedemption.jpg", MovieCategory.Action, 1, 1),
                        new Movie("Ghost", "This is the Ghost movie description", DateTime.Now, DateTime.Now.AddDays(7), 39.50, "/images/movies/Ghost.jpg", MovieCategory.Horror, 4, 4),
                        new Movie("Race", "This is the Race movie description", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-5), 39.50, "/images/movies/Race.jpg", MovieCategory.Documentary, 1, 2),
                        new Movie("Scoob", "This is the Scoob movie description", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-2), 39.50, "/images/movies/scoob.jpg", MovieCategory.Cartoon, 1, 3),
                        new Movie("Cold Soles", "This is the Cold Soles movie description", DateTime.Now.AddDays(3), DateTime.Now.AddDays(20), 39.50, "/images/movies/ColdSoles.jpg", MovieCategory.Drama, 1, 5)
                    });
                    await context.SaveChangesAsync();
                }

                // 5. Actors & Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie(1, 1),
                        new Actor_Movie(1, 3),
                        new Actor_Movie(2, 1),
                        new Actor_Movie(2, 4),
                        new Actor_Movie(3, 1),
                        new Actor_Movie(3, 2),
                        new Actor_Movie(3, 5),
                        new Actor_Movie(4, 2),
                        new Actor_Movie(4, 3),
                        new Actor_Movie(4, 4),
                        new Actor_Movie(5, 2),
                        new Actor_Movie(5, 3),
                        new Actor_Movie(5, 4),
                        new Actor_Movie(5, 5),
                        new Actor_Movie(6, 3),
                        new Actor_Movie(6, 4),
                        new Actor_Movie(6, 5)
                    });
                    await context.SaveChangesAsync();
                }

                await SeedUsersAndRolesAsync(userManager, roleManager);
            }
        }

        public static async Task SeedUsersAndRolesAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            var roles = new[] { "Admin", "Editor", "User" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = roleName
                    });
                }
            }

            await CreateUserAsync(
                userManager,
                email: "admin@admin.com",
                userName: "admin@admin.com", 
                personName: "Admin User",
                role: "Admin");

            await CreateUserAsync(
                userManager,
                email: "editor@editor.com",
                userName: "editor@editor.com", 
                personName: "Editor User",
                role: "Editor");

            await CreateUserAsync(
                userManager,
                email: "user@user.com",
                userName: "user@user.com", 
                personName: "Standard User",
                role: "User");
        }

        private static async Task CreateUserAsync(
            UserManager<ApplicationUser> userManager,
            string email,
            string userName,
            string personName,
            string role)
        {
            const string seedPassword = "aA11111111#S@";
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                var user = new ApplicationUser
                {
                    Email = email,
                    UserName = userName,
                    PersonName = personName,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, seedPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(error => error.Description));
                    throw new InvalidOperationException($"Failed to create seed user '{email}': {errors}");
                }

                existingUser = user;
            }
            else
            {
                var requiresUpdate = false;

                if (existingUser.UserName != userName)
                {
                    existingUser.UserName = userName;
                    requiresUpdate = true;
                }

                if (existingUser.PersonName != personName)
                {
                    existingUser.PersonName = personName;
                    requiresUpdate = true;
                }

                if (!existingUser.EmailConfirmed)
                {
                    existingUser.EmailConfirmed = true;
                    requiresUpdate = true;
                }

                if (requiresUpdate)
                {
                    var updateResult = await userManager.UpdateAsync(existingUser);
                    if (!updateResult.Succeeded)
                    {
                        var errors = string.Join(", ", updateResult.Errors.Select(error => error.Description));
                        throw new InvalidOperationException($"Failed to update seed user '{email}': {errors}");
                    }
                }

                var hasExpectedPassword = await userManager.CheckPasswordAsync(existingUser, seedPassword);
                if (!hasExpectedPassword)
                {
                    var resetToken = await userManager.GeneratePasswordResetTokenAsync(existingUser);
                    var resetResult = await userManager.ResetPasswordAsync(existingUser, resetToken, seedPassword);

                    if (!resetResult.Succeeded)
                    {
                        var errors = string.Join(", ", resetResult.Errors.Select(error => error.Description));
                        throw new InvalidOperationException($"Failed to reset password for seed user '{email}': {errors}");
                    }
                }
            }

            if (!await userManager.IsInRoleAsync(existingUser, role))
            {
                await userManager.AddToRoleAsync(existingUser, role);
            }
        }
    }
}
