using System;

namespace MyPro.App.Core.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken<TKey>(TKey userId, string firstName, string lastName) where TKey : struct;
}

