using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommonData
    {
        public class AppSettings
        {
            public OpenAIConfig OpenAI { get; set; }
        }

        public class OpenAIConfig
        {
            public string ApiAddress { get; set; }
            public string ApiKey { get; set; }
            public string ApiModel { get; set; }
        }
    }
}
