using System;
using MyPro.App.Infrastructure.Options;

namespace MyPro.App.Infrastructure.Settings
{
    public class MyProAppOptions
    {
        public string Name { get; set; }
        public MyProDatabaseOptions Database { get; set; }
    }
}

