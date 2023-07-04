// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static List<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource("shopping-api")
            {
                Scopes =
                {
                    "read"
                }
            },
            new ApiResource("gateway-api")
            {
                Scopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "read"
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                //new ApiScope(IdentityServerConstants.StandardScopes.OpenId),
                //new ApiScope(IdentityServerConstants.StandardScopes.Profile),
                //new ApiScope(IdentityServerConstants.StandardScopes.Email),
                new ApiScope("read")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "public-api",
                    ClientName= "Public API - Web/Mobile to Server communication",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "read"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:6001",
                        "http://localhost:7001",
                        "http://localhost:8001",
                        "http://localhost:9001",
                        "https://localhost:6443",
                        "https://localhost:8443",
                        "https://localhost:6443",
                        "https://localhost:9443"
                    },
                    RedirectUris =
                    {
                        "https://localhost:6443/swagger/oauth2-redirect.html",
                        "https://www.getpostman.com/oauth2/callback"
                    },
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RequireConsent = false,
                    AlwaysIncludeUserClaimsInIdToken = true
                },
                new Client
                {
                    ClientId = "private-api",
                    ClientName = "Private API - Machine to machine communication",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RedirectUris = {
                        "http://localhost:6001/swagger/oauth2-redirect.html",
                        "http://localhost:7001/swagger/oauth2-redirect.html",
                        "http://localhost:8001/swagger/oauth2-redirect.html",
                        "http://localhost:9001/swagger/oauth2-redirect.html",
                        "https://localhost:6443/swagger/oauth2-redirect.html",
                        "https://localhost:7443/swagger/oauth2-redirect.html",
                        "https://localhost:8443/swagger/oauth2-redirect.html",
                        "https://localhost:9443/swagger/oauth2-redirect.html",
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    RequireClientSecret = true,
                    RequireConsent = false,
                    AllowedScopes = { "read" },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:6001",
                        "http://localhost:7001",
                        "http://localhost:8001",
                        "http://localhost:9001",
                        "https://localhost:6443",
                        "https://localhost:8443",
                        "https://localhost:7443",
                        "https://localhost:9443"
                    },
                    AccessTokenLifetime = 86400
                }
            };
    }
}