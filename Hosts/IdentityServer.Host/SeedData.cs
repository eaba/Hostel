using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServer.Host.Data;
using IdentityServer.Host.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Host
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            //Console.WriteLine("Seeding database...");

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                {
                    var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                    context.Database.Migrate();
                    EnsureSeedData(context, configuration);
                }

                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<HostelUser>>();
                    var alice = userMgr.FindByNameAsync("fake@gmail.com").Result;
                    if (alice == null)
                    {
                        alice = new HostelUser
                        {
                            UserName = "fake@gmail.com",
                            Email = "fake@gmail.com",
                            EmailConfirmed = true,
                            PhoneNumber = "2348136786808",
                            PhoneNumberConfirmed = true,
                            TwoFactorEnabled = true
                        };
                        var result = userMgr.CreateAsync(alice, "alicechukwu").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Role, "Tester"),
                            new Claim(JwtClaimTypes.PreferredUserName, "fake@gmail.com"),
                            new Claim(JwtClaimTypes.Name, "Ebere Abanonu"),
                            new Claim(JwtClaimTypes.GivenName, "Ebere"),
                            new Claim(JwtClaimTypes.FamilyName, "Abanonu"),
                            new Claim(JwtClaimTypes.Email, "fake@yahoo.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                            new Claim("TimeZone", "W. Central Africa Standard Time"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        //Console.WriteLine("alice created");
                    }
                    else
                    {
                        //Console.WriteLine("alice already exists");
                    }
                }
            }

            //Console.WriteLine("Done seeding database.");
            //Console.WriteLine();
        }
        
        private static void EnsureSeedData(ConfigurationDbContext context, IConfiguration configuration)
        {
            try
            {
                if (!context.Clients.Any())
                {
                    //Console.WriteLine("Clients being populated");
                    foreach (var client in Config.GetClients(configuration).ToList())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }
                else
                {
                    //Console.WriteLine("Clients already populated");
                }

                if (!context.IdentityResources.Any())
                {
                    //Console.WriteLine("IdentityResources being populated");
                    foreach (var resource in Config.GetIdentityResources().ToList())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                else
                {
                    //Console.WriteLine("IdentityResources already populated");
                }

                if (!context.ApiResources.Any())
                {
                    //Console.WriteLine("ApiResources being populated");
                    foreach (var resource in Config.GetApiResources().ToList())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                else
                {
                    //Console.WriteLine("ApiResources already populated");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
