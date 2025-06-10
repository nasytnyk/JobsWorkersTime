using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsWorkersTime.ModelAbstract
{
    public static class Config
    {
        private static IConfigurationRoot _configRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static int SimulationSpeed => _configRoot.GetValue<int>("Simulation:Speed");

    }
}
