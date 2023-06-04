using System;
namespace MyPro.App.Core.Authentication;

public record class AuthenticationResult<TKey>(TKey id, string firstName, string lastName, string email, string token);

