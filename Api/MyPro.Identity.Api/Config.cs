// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static List<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource("api"){ Scopes = { "api" } },
            new ApiResource("web"){ Scopes = { "web" } }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("openid"),
                new ApiScope("profile"),
                new ApiScope("email"),
                new ApiScope("read"),
                new ApiScope("write"),
                new ApiScope("web"),
                new ApiScope("api")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "web",
                    AllowedGrantTypes = new [] { GrantType.AuthorizationCode },
                    AllowedScopes = { "openid", "profile", "web" },
                    AllowedCorsOrigins = { "https://localhost:5000" },
                    RedirectUris = { "https://localhost:5000/swagger/oauth2-redirect.html" },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RequirePkce = true,
                },
                new Client
                {
                    ClientId = "api",
                    ClientName = "API",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RedirectUris = { "https://localhost:5002/swagger/oauth2-redirect.html" },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RequireClientSecret = true,
                    RequireConsent = false,
                    AllowedScopes = { "api" },
                    AllowedCorsOrigins = { "https://localhost:5002" },
                    AccessTokenLifetime = 86400
                }
            };
    }
}