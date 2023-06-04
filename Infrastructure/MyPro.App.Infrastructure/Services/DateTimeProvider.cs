using System;
using MyPro.App.Core.Contracts.Services;

namespace MyPro.App.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}

