using System;

namespace MyPro.App.Core.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult<TKey> Register<TKey>(string firstName, string lastName, string password, string email);
    AuthenticationResult<TKey> Login<TKey>(string username, string password);
}

