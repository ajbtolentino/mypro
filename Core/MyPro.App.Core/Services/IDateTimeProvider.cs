using System;
namespace MyPro.App.Core.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}

