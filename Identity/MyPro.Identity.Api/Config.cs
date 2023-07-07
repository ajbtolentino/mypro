// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using IdentityModel;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Text.Json;

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
            new ApiResource("catalog-api")
            {
                Scopes =
                {
                    "read"
                }
            },
            new ApiResource("order-api")
            {
                Scopes =
                {
                    "read"
                }
            },
            new ApiResource("payment-api")
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
                    "read"
                }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
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

        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };

                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "818727",
                        Username = "alice@test.com",
                        Password = "Test.123",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "88421113",
                        Username = "bob@test.com",
                        Password = "Test.123",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    }
                };
            }
        }
    }
}