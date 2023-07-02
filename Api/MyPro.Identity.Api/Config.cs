// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static List<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource
            {
                Name = "identity-server-demo-api",
                DisplayName = "Identity Server Demo API",
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("openid"),
                new ApiScope("profile"),
                new ApiScope("email"),
                new ApiScope("read"),
                new ApiScope("write"),
                new ApiScope("api1"),
                new ApiScope("identity-server-demo-api")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                //new Client
                //{
                //    ClientId = "client",
                //    ClientSecrets = { new Secret("secret".Sha256()) },
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    AllowedCorsOrigins = {"https://localhost:5000"},
                //    AllowedScopes = { "api1" }
                //},
                new Client
                {
                    ClientId = "identity-server-demo-web",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { "http://localhost:5000/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:5000/" },
                    AllowedScopes = { "identity-server-demo-api", "write", "read", "openid", "profile", "email" },
                    AllowedCorsOrigins = {"https://localhost:5000"},
                    AccessTokenLifetime = 86400
                }
                //new Client
                //{
                //    ClientId = "gateway-client",
                //    ClientSecrets = { new Secret("secret".Sha256()) },
                //    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                //    AllowedCorsOrigins = {"https://localhost:5000"},
                //    AllowedScopes = { "api1" }
                //},
                //new Client
                //{
                //    ClientId = "mvc",
                //    ClientSecrets = { new Secret("secret".Sha256()) },
                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequirePkce = true,
                //    RedirectUris = {"https://localhost:5000/swagger/oauth2-redirect.html"},
                //    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                //    AllowedCorsOrigins = {"https://localhost:5000"},
                //    AllowedScopes = new List<string>
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        "api1"
                //    }
                //},
                //new Client
                //{
                //    ClientId = "gateway_api_swagger",
                //    ClientName = "Swagger UI for gateway_api",
                //    ClientSecrets = {new Secret("secret".Sha256())}, // change me!
                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequirePkce = true,
                //    RequireClientSecret = false,
                //    RedirectUris = {"https://localhost:5000/swagger/oauth2-redirect.html"},
                //    AllowedCorsOrigins = {"https://localhost:5000"},
                //    AllowedScopes = {"api1"}
                //}
            };
    }
}