using System;
using MyPro.App.Core.Services;

namespace MyPro.App.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

