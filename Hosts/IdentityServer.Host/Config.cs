using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
namespace IdentityServer.Host
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("portal", "Hostel Portal")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {            // client credentials client
            return new List<Client>
            {                
                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "67b7af6f-aff1-4b86-8811-89d9f671gh56",
                    ClientName = "Hostel Portal",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    RequireConsent = false,
                    ClientSecrets =
                    {
                        new Secret("y67gf7d2-c60a-40a7-9675-8df465b77nn7".Sha256())
                    },

                    RedirectUris = { $"https://portal.hostel.com/signin-oidc" },
                    PostLogoutRedirectUris = { "https://hostel.com" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "web"
                    },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 43200, //12 hours
                    IdentityTokenLifetime = 43200,//12 hours
                    AuthorizationCodeLifetime = 43200,
                    DeviceCodeLifetime = 43200,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    EnableLocalLogin = true,
                    AccessTokenType = AccessTokenType.Jwt
                }
            };
        }
    }
}
