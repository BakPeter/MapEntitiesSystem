using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsRepositoryService.Infrastructure.MinIoConfiguration
{
    public class Configuration
    {
        public string Server { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string MapsBucket { get; set; } = string.Empty;

    }
}
