using System;
using Microsoft.Extensions.Options;

namespace MyPro.App.Infrastructure.Options
{
    public class MyProDatabaseOptions
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}

